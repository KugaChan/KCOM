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

//#define FIX_HEX_PATH

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

		//打印发送的数据
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
		TEXT("\\\\.\\Pipe\\fast_printf_pipe"),            //管道名
		PIPE_ACCESS_DUPLEX,                  //管道类型
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,  //管道参数
		PIPE_UNLIMITED_INSTANCES,						//管道能创建的最大实例数量
		0,												//输出缓冲区长度 0表示默认
		0,												//输入缓冲区长度 0表示默认
		NMPWAIT_WAIT_FOREVER,							//超时时间
		NULL);											//指定一个SECURITY_ATTRIBUTES结构,或者传递零值

	if(INVALID_HANDLE_VALUE == hPipe)
	{
		printf("Create Pipe Error(%d)\n", GetLastError());
	}
	else
	{
		printf("Waiting For Client Connection...\n");

		if(!ConnectNamedPipe(hPipe, NULL))  //阻塞等待客户端连接。
		{
			printf("Connection failed!\n");
		}
		else
		{
			printf("Connection Success!\n");
		}


		while(true)
		{
			if(!ReadFile(hPipe, rbuf, 256, &rlen, NULL)) //接受客户端发送过来的内容
			{
				printf("Read Data From Pipe Failed!\n");
				break;
			}
			else
			{
				#if 0
					//打印收到的数据
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
					wlen = 128;	//512 X 4 = 2048, 翻4倍

					//生成发送的数据
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
					//打印发送的数据
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

				WriteFile(hPipe, wbuf, wlen, &wlen, 0);  //向客户端发送内容
				FlushFileBuffers(hPipe);
			}
		}
		
		DisconnectNamedPipe(hPipe);
		CloseHandle(hPipe);//关闭管道

		free(rbuf);
		free(wbuf);
		#if 1
			free(mi_fa);
			free(mi_fb);
		#endif
	}

	//system("pause");
	return 0;
}

