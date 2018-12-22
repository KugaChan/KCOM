#define _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_DEPRECATE

//#include "stdafx.h"

//#include <tchar.h>
#include  <direct.h>
#include <stdio.h>
#include <windows.h>
#include <ctime>

#include "sys.h"
#include "readhex.h"

int main(int argc, char* argv[])
{
#if 1
	char buffer[MAX_PATH];
	_getcwd(buffer, MAX_PATH);
	printf("working path: %s\n", buffer);
#endif

	for(int i = 0; i < argc; i++)
	{
		printf("argc[%x]:%x --- %s\r\n", i, argv[i], argv[i]);
	}

	#if 1
		if(argc < 3)
		{
			printf("please input hex path\r\n");
			return 0;
		}

		#if 1
			char path_fa[MAX_PATH];
			char path_fb[MAX_PATH];

			strcpy(path_fa, argv[1]);
			strcpy(path_fb, argv[2]);
		#else
			char path_fa[MAX_PATH] = ".\\DEBUG\\HEX\\cpu0.hex";
			char path_fb[MAX_PATH] = ".\\DEBUG\\HEX\\cpu1.hex";
		#endif

		#define kReadBufSize	(1UL << 20)	//1MB

		memory_desc *mi_fa = (memory_desc*)malloc(MI_TOTAL_SIZE);
		memory_desc *mi_fb = (memory_desc*)malloc(MI_TOTAL_SIZE);

		hex2bin_mount(mi_fa);
		hex2bin_mount(mi_fb);
		if(hex2bin_read_hex(path_fa, mi_fa) == 0)
		{
			printf("###read fa:%s fail!\n", path_fa);
			return 0;
		}

		if(hex2bin_read_hex(path_fb, mi_fb) == 0)
		{
			printf("###read fb:%s fail!\n", path_fb);
			return 0;
		}
	#endif

	char *rbuf;
	char *wbuf;

	rbuf = (char*)malloc(1024 * 1024);//1MB
	wbuf = (char*)malloc(1024 * 1024);//1MB
	DWORD rlen;
	DWORD wlen;

	#if 0
		char real_data[] = { 0x99, 0x1E, 0x37, 0xC3, 0xEC, 0xEE, 0xFF, 0x5, 0x9, 0x0,
			0xF1, 0xD1, 0x2, 0xB2, 0x7, 0x1, 0x1, 0x8, 0x0, 0x0, 0x0, 0x0, 0x16, 0x0, 0x0, 0x0, 0xCA, 0x1, 0x0, 0x0 };

		rlen = 30;
		wlen = StringConvert((BYTE*)real_data, rlen, (BYTE*)wbuf, mi_fa, mi_fb);

		//��ӡ���͵�����
		printf("To Client[%d]:", wlen);
		for(DWORD i = 0; i < wlen; i++)
		{
			if(i % 16 == 0)
			{
				printf("\r\n");
			}
			printf(" %02x", (BYTE)wbuf[i]);
		}
		printf("\r\n");

		while(1);
	#endif

	HANDLE hPipe = CreateNamedPipe(
		TEXT("\\\\.\\Pipe\\fast_printf_pipe"),            //�ܵ���
		PIPE_ACCESS_DUPLEX,                  //�ܵ�����
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,  //�ܵ�����
		PIPE_UNLIMITED_INSTANCES,						//�ܵ��ܴ��������ʵ������
		0,												//������������� 0��ʾĬ��
		0,												//���뻺�������� 0��ʾĬ��
		NMPWAIT_WAIT_FOREVER,							//��ʱʱ��
		NULL);											//ָ��һ��SECURITY_ATTRIBUTES�ṹ,���ߴ�����ֵ

	if(INVALID_HANDLE_VALUE == hPipe)
	{
		printf("Create Pipe Error(%d)\n", GetLastError());
	}
	else
	{
		printf("Waiting For Client Connection...\n");

		if(!ConnectNamedPipe(hPipe, NULL))  //�����ȴ��ͻ������ӡ�
		{
			printf("Connection failed!\n");
		}
		else
		{
			printf("Connection Success!\n");
		}


		while(true)
		{
			if(!ReadFile(hPipe, rbuf, 256, &rlen, NULL)) //���ܿͻ��˷��͹���������
			{
				printf("Read Data From Pipe Failed!\n");
				break;
			}
			else
			{
				#if 0
					//��ӡ�յ�������
					printf("From Client[%d]:", rlen);
					for(DWORD i = 0; i < rlen; i++)
					{
						if(i % 16 == 0)
						{
							printf("\r\n");
						}
						printf(" %02x", (BYTE)rbuf[i]);
					}
					printf("\r\n");
				#endif

				#if 1
					wlen = StringConvert((BYTE*)rbuf, rlen, (BYTE*)wbuf, mi_fa, mi_fb);
					//printf("To Client[%d]:", wlen);
				#else
					wlen = 128;	//512 X 4 = 2048, ��4��

					//���ɷ��͵�����
					for(DWORD i = 0; i < wlen; i++)
					{
						wbuf[i] = i % 256;
					}
				#endif
				if(wlen == 0)
				{
					wbuf[0] = 0x00;
					wlen = 1;
				}

				#if 0
					//��ӡ���͵�����
					printf("To Client[%d]:", wlen);
					for(DWORD i = 0; i < wlen; i++)
					{
						if(i % 16 == 0)
						{
							printf("\r\n");
						}
						printf(" %02x", (BYTE)wbuf[i]);
					}
					printf("\r\n");
				#endif

				WriteFile(hPipe, wbuf, wlen, &wlen, 0);  //��ͻ��˷�������
				FlushFileBuffers(hPipe);
			}
		}
		
		DisconnectNamedPipe(hPipe);
		CloseHandle(hPipe);//�رչܵ�

		free(rbuf);
		free(wbuf);
		free(mi_fa);
		free(mi_fb);
	}

	//system("pause");
	return 0;
}

