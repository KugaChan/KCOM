#define _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_DEPRECATE

#include <tchar.h>

#include <stdio.h>
#include <windows.h>
#include <ctime>

#include "sys.h"
#include "readhex.h"


/**
***********************************************************************************
*@brief		安装  内存初始化等
*@param[in]
*@param[out]
*@return
*@warning
*@see
************************************************************************************
*/
void hex2bin_mount(memory_desc *mi)
{
	DWORD offset = 0;
	DWORD BankNum = 0;
	DWORD size = MEMORY_MAX_BANK*MEMORY_BANK_SIZE;

	//mi->pBuff = (BYTE*)(mi + 1);					//4MB, 在外面申请好了
	mi->bank_max = 0;
	mi->bank_used = 0;
	memset(mi->pBuff, 0, size);
	while(1)
	{
		if(offset + MEMORY_BANK_SIZE > size)
		{
			break;
		}
		if(BankNum >= MEMORY_MAX_BANK)
		{
			break;
		}
		mi->bank[BankNum].p = mi->pBuff + offset;
		mi->bank[BankNum].Address = 0;
		mi->bank[BankNum].pSize = MEMORY_BANK_SIZE;
		mi->bank[BankNum].BinSz = 0;

		offset += MEMORY_BANK_SIZE;
		BankNum++;
	}
	mi->bank_max = BankNum;
}


/**
***********************************************************************************
*@brief		内存记录
*@param[in]
*@param[out]
*@return
*@warning
*@see
************************************************************************************
*/
static void hex2bin_mem_store(memory_desc *mi, DWORD Addr, BYTE *p, DWORD Len)
{
	DWORD BankNum;
	memory_bank *pbank = NULL;
	for(BankNum = 0; BankNum < mi->bank_used; BankNum++)
	{
		if((Addr >= mi->bank[BankNum].Address) &&
			((Addr - mi->bank[BankNum].Address + Len) <= mi->bank[BankNum].pSize))
		{
			pbank = &mi->bank[BankNum];
			break;
		}
	}
	if(pbank == NULL)
	{
		if(mi->bank_used < mi->bank_max)
		{
			pbank = &mi->bank[mi->bank_used];
			pbank->Address = Addr;
			pbank->BinSz = 0;

			mi->bank_used++;
		}
	}
	if((pbank != NULL) && (p != NULL))
	{
		memcpy(pbank->p + (Addr - pbank->Address), p, Len);
		pbank->BinSz += Len;
	}
}








#define RECORD_EXTENDED_LINEAR_ADDRESS 0x04
#define RECORD_SET_EXECUTION_START 0x05

static int get_nybble(unsigned char *s)
{
	int value;

	if(*s >= '0' && *s <= '9') {
		value = *s - '0';
	}
	else if(*s >= 'a' && *s <= 'f') {
		value = *s - 'a' + 10;
	}
	else if(*s >= 'A' && *s <= 'F') {
		value = *s - 'A' + 10;
	}
	else {
		printf("Invalid hex character: %c\n", *s);
		value = 0;
	}

	return value;
}

static int get_byte(unsigned char *s)
{
	/* this is right, shut up: */
	return get_nybble(s) * 16 + get_nybble(s + 1);
}

static int get_word(unsigned char *s)
{
	/* yes this too */
	return get_byte(s) * 256 + get_byte(s + 2);
}



/**
***********************************************************************************
*@brief		读取hex文件到mi中
*@param[in]
*@param[out]
*@return
*@warning
*@see
************************************************************************************
*/
int hex2bin_read_hex(char *file_name, memory_desc *mi)
{
	unsigned char *s;
	int base_address = 0x0;
	int start_execution = -1;
	int num_bytes;
	int address;
	unsigned char checksum;
	int i;
	int byte;
	char line[256];
	int line_number = 0;
	int skipped_bytes = 0;
	int res = 0;
	FILE *f;

	f = fopen(file_name, "r+");
	if(f == NULL)
	{
		return 0;
	}

	while(fgets(line, sizeof(line), f) != NULL)
	{
		s = (unsigned char *)line;
		line_number++;

		// Must start with colon.
		if(*s != ':')
		{
			printf("Bad format on line %d: %s\n", line_number, line);
			res = 0;
			break;
		}
		s++;

		checksum = 0;
		num_bytes = get_byte(s);
		if(num_bytes == 0)
		{
			// All done.
			res = 1;
			break;
		}

		checksum += (unsigned char)num_bytes;
		s += 2;

		address = get_word(s);

		checksum += (unsigned char)get_byte(s);
		checksum += (unsigned char)get_byte(s + 2);
		s += 4;

		if((byte = get_byte(s)) != 0)
		{
			s += 2;
			checksum += (unsigned char)byte;

			if(byte == RECORD_SET_EXECUTION_START)
			{
				start_execution = get_word(s) * 65536 + get_word(s + 4);
				checksum += (unsigned char)get_byte(s);
				checksum += (unsigned char)get_byte(s + 2);
				checksum += (unsigned char)get_byte(s + 4);
				checksum += (unsigned char)get_byte(s + 6);
				s += 8;
			}
			else if(byte == RECORD_EXTENDED_LINEAR_ADDRESS)
			{
				base_address = get_word(s) * 65536;
				checksum += (unsigned char)get_byte(s);
				checksum += (unsigned char)get_byte(s + 2);
				s += 4;

				hex2bin_mem_store(mi, base_address, NULL, 0);
				printf("bank:%x\n", base_address);
			}
			else
			{
				printf("record type other than 0 or 4\n");
				res = 0;
				break;
			}
		}
		else
		{
			// byte doesn't affect checksum because it's 0
			BYTE pBuff[256];
			s += 2;
			for(i = 0; i < num_bytes; i++)
			{
				byte = get_byte(s);
				s += 2;
				checksum += (unsigned char)byte;
				pBuff[i] = byte;
			}
			hex2bin_mem_store(mi, base_address + address, pBuff, num_bytes);
		}

		// Verify checksum.
		checksum = ~checksum + 1;
		int file_checksum = get_byte(s);
		if(checksum != file_checksum)
		{
			printf("Checksum mismatch on line %d: %02x vs %02x\n",
				line_number, checksum, file_checksum);
		}
	}

	if(skipped_bytes > 0)
	{
		printf("Skipped %d bytes.\n", skipped_bytes);
	}

	if(start_execution >= 0)
		printf("start execution at 0x%08X\n", start_execution);


	fclose(f);

	if(res == 1)
	{
		mi->bin_start_execution = start_execution;
		printf("Hex2bin read success\n");
	}

	return res;
}




/**
***********************************************************************************
*@brief
*@param[in]
*@param[out]
*@return
*@warning
*@see
************************************************************************************
*/
char * Addr2String(memory_desc *mi, DWORD dwAddr)
{
	DWORD  BankNum;
	char * pStr = "$Not Find\n";
	for(BankNum = 0; BankNum < mi->bank_used; BankNum++)
	{
		if((dwAddr >= mi->bank[BankNum].Address) &&
			((dwAddr - mi->bank[BankNum].Address) <= mi->bank[BankNum].pSize))
		{
			pStr = (char*)mi->bank[BankNum].p + (dwAddr - mi->bank[BankNum].Address);
			break;
		}
	}

	if( /*(BankNum == mi->bank_used) ||		//在HEX中找不到*/
		((*pStr <= 1) && (*pStr >= 0x7F)))	//第一个字符必须是ASCII码
	{
		pStr = "$Format Error\n";
	}
	return pStr;
}

#if 0
	#define ConvPrintf	printf
#else
	#define ConvPrintf(...)
#endif

static  BYTE	bSrcTab[512];
static  DWORD   dwSrcLen = 0;
static  DWORD	dwHopLen = 0;
static  DWORD	dwAnalysisSrcCnt = 0;
static  DWORD	dwAnalysisOutCnt = 0;
static  DWORD	dwAnalysisChecksumErrCnt = 0;
/**
***********************************************************************************
*@brief
*@param[in]
*@param[out]
*@return
*@warning
*@see
************************************************************************************
*/
u32 StringConvert(u8 *pSrc, u32 dwLen, u8 *pOutStr, memory_desc *mi_fa, memory_desc *mi_fb)
{
	//打印发送的数据
	ConvPrintf("Cvt[%d]:", dwLen);
	for(u32 i = 0; i < dwLen; i++)
	{
		if(i % 16 == 0)
		{
			ConvPrintf("\r\n");
		}
		ConvPrintf(" %02x", pSrc[i]);
	}
	ConvPrintf("\r\n");

	DWORD dwOutLen = 0;
	DWORD i = 0;
	while(i < dwLen)
	{
		BYTE data = *pSrc;

		bool tag = false;
		if(dwSrcLen == 0)
		{
			if((data == 0x98) || data == 0x99)
			{
				bSrcTab[dwSrcLen] = data;
				dwSrcLen++;
				dwHopLen = 5;
				tag = true;
			}
		}
		else if(dwSrcLen < dwHopLen)
		{
			bSrcTab[dwSrcLen] = data;
			if(dwSrcLen == 1)
			{
				dwHopLen = data;
			}
			dwSrcLen++;
			tag = true;
		}

		if(tag == false)
		{
			pOutStr[dwOutLen++] = data;
		}
		pSrc++;
		i++;

		if((dwSrcLen > 0) && (dwSrcLen >= dwHopLen))
		{
			DWORD n;
			BYTE checksum = 0;
			for(n = 0; n < dwSrcLen; n++)
			{
				checksum += bSrcTab[n];
			}
			if(checksum == 0)
			{
				memory_desc *mi = bSrcTab[0] == 0x98 ? mi_fa : mi_fb;
				DWORD dwpArgBuff[64];
				BYTE *pbArgBuff = (BYTE *)&dwpArgBuff[0];
				DWORD dwZipBipmap = bSrcTab[3] & 0xFC;
				DWORD dwZipBipNum = 0;
				n = (bSrcTab[3] & 0x03);
				for(DWORD i = 0; i < n; i++)
				{
					dwZipBipmap |= bSrcTab[4 + i] << (i * 8 + 8);
				}
				n += 4;
				for(;;)
				{
					if((dwZipBipNum < 32) && (dwZipBipmap & (1UL << dwZipBipNum)))
					{
						*pbArgBuff = 0;
						pbArgBuff++;
					}
					else if(n < dwSrcLen)
					{
						*pbArgBuff = bSrcTab[n];
						pbArgBuff++;
						n++;
					}
					dwZipBipNum++;
					if((dwZipBipNum >= 32) && (n >= dwSrcLen))
					{
						break;
					}
				}

				char *format = Addr2String(mi, dwpArgBuff[0]);
				char *Str_t = format;
				DWORD dwTemp = 1;
				while(*Str_t != 0)
				{
					if(*Str_t == '%')
					{
						Str_t++;
						if(*Str_t == 's')
						{
							dwpArgBuff[dwTemp] = (DWORD)Addr2String(mi, dwpArgBuff[dwTemp]);
						}
						dwTemp++;
					}
					else
					{
						Str_t++;
					}
				}
				dwOutLen += _vsnprintf((char*)&pOutStr[dwOutLen], 4096, format, (va_list)&dwpArgBuff[1]);
			}
			else
			{
				//校验码错误的时候，跳过标记头，退回到原点的下一个BYTE，重新寻找标记
				dwAnalysisChecksumErrCnt++;
				ConvPrintf("checksum error,skip %d\n", dwAnalysisChecksumErrCnt);
				pSrc -= dwSrcLen - 1;
				i -= dwSrcLen - 1;
			}
			dwSrcLen = 0;
			dwHopLen = 0;
		}
	}

	dwAnalysisSrcCnt += dwLen;
	dwAnalysisOutCnt += dwOutLen;
	ConvPrintf("Analysis Src:%d Out:%d  %d\n", dwAnalysisSrcCnt, dwAnalysisOutCnt, dwAnalysisOutCnt / dwAnalysisSrcCnt);

	return dwOutLen;
}









