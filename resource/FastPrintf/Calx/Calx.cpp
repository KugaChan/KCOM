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


BOOL APIENTRY DllMain(
	HANDLE hModule,             // DLLģ��ľ��
	DWORD ul_reason_for_call,   // ���ñ�������ԭ��
	LPVOID lpReserved           // ����
)
{
	switch(ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		//�������ڼ��ر�DLL
		break;
		case DLL_THREAD_ATTACH:
		//һ���̱߳�����
		break;
		case DLL_THREAD_DETACH:
		//һ���߳������˳�
		break;
		case DLL_PROCESS_DETACH:
		//��������ж�ر�DLL
		break;
	}
	return TRUE;            //����TRUE,��ʾ�ɹ�ִ�б�����
}

#ifndef USE_FOR_EXPORT_DLL

#define FIX_HEX_PATH

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

	system("pause");

	#if 1
		#ifndef FIX_HEX_PATH
			if(argc < 3)
			{
				printf("please input hex path\r\n");
				return 0;
			}
		#endif

		#ifndef FIX_HEX_PATH
			char path_fa[MAX_PATH];
			char path_fb[MAX_PATH];

			strcpy(path_fa, argv[1]);
			strcpy(path_fb, argv[2]);
		#else
			char path_fa[MAX_PATH] = "..\\HEX\\cpu0.hex";
			char path_fb[MAX_PATH] = "..\\HEX\\cpu1.hex";
		#endif

		#define kReadBufSize	(1UL << 20)	//1MB

		memory_desc *mi_fa = (memory_desc*)malloc(sizeof(memory_desc));
		memory_desc *mi_fb = (memory_desc*)malloc(sizeof(memory_desc));

		mi_fa->pBuff = (u8*)malloc(MEMORY_MAX_BANK*MEMORY_BANK_SIZE);
		mi_fb->pBuff = (u8*)malloc(MEMORY_MAX_BANK*MEMORY_BANK_SIZE);

		mi_fa->bank = (memory_bank*)malloc(MEMORY_MAX_BANK*sizeof(memory_bank));
		mi_fb->bank = (memory_bank*)malloc(MEMORY_MAX_BANK*sizeof(memory_bank));

		hex2bin_mount(mi_fa);
		hex2bin_mount(mi_fb);
		if(hex2bin_read_hex(path_fa, mi_fa) == 0)
		{
			printf("###read fa:%s fail!\n", path_fa);
			system("pause");
		}

		if(hex2bin_read_hex(path_fb, mi_fb) == 0)
		{
			printf("###read fb:%s fail!\n", path_fb);
			system("pause");
		}
	#endif

	char *rbuf;
	char *wbuf;

	rbuf = (char*)malloc(1024 * 1024);//1MB
	wbuf = (char*)malloc(1024 * 1024);//1MB
	DWORD rlen;
	DWORD wlen;



	#if 1
		//char real_data[] = { 0x99, 0x1E, 0x37, 0xC3, 0xEC, 0xEE, 0xFF, 0x5, 0x9, 0x0,
		//	0xF1, 0xD1, 0x2, 0xB2, 0x7, 0x1, 0x1, 0x8, 0x0, 0x0, 0x0, 0x0, 0x16, 0x0, 0x0, 0x0, 0xCA, 0x1, 0x0, 0x0 };

	char real_data[] = {
		0x99 , 0x1d , 0xce , 0xe3 , 0xec , 
		0xee , 0xff , 0x02 , 0x09 , 0x00 , 
		0xf1 , 0xce , 0xb2 , 0x07 , 0x01 , 
		0x01 , 0x08 , 0x00 , 0x00 , 0x00 , 
		0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 
		0x28 , 0x0b , 0x00 , 0x00 };

		rlen = 29;
		wlen = StringConvert((BYTE*)real_data, rlen, (BYTE*)wbuf, mi_fa, mi_fb);

		//��ӡ���͵�����
		printf("To Client[%d]:", wlen);
		for(DWORD i = 0; i < wlen; i++)
		{
			if(i % 16 == 0)
			{
				printf("\r\n");
			}
			//printf(" %02x", (BYTE)wbuf[i]);
			printf(" %c", (BYTE)wbuf[i]);
		}
		printf("\r\n");

		free(rbuf);
		free(wbuf);
		#if 1
			free(mi_fa->bank);
			free(mi_fb->bank);
			free(mi_fa);
			free(mi_fb);
		#endif

		system("pause");
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

		if(!ConnectNamedPipe(hPipe, NULL))  //�����ȴ��ͻ�������
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
		#if 1
			free(mi_fa->bank);
			free(mi_fb->bank);
			free(mi_fa);
			free(mi_fb);
		#endif
	}

	//system("pause");
	return 0;
}
#endif

