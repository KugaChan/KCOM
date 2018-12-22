
#ifndef __readhex_h__
#define __readhex_h__

// Parses an Intel HEX file.
//
// https://en.wikipedia.org/wiki/Intel_HEX
//
#define MAX_PATH          260

#define		MEMORY_MAX_BANK		64			//64 * 64KB = 4MB
#define		MEMORY_BANK_SIZE	(65536)

typedef struct memory_bank
{
	unsigned char	*p;
	DWORD			pSize;

	DWORD			Address;
	DWORD			BinSz;
}memory_bank;


typedef struct memory_desc
{
	memory_bank		bank[MEMORY_MAX_BANK];
	DWORD			bank_max;					//记录mount内存段的数量
	DWORD			bank_used;					//当前用到的第几段
	BYTE			*pBuff;
	DWORD			bin_start_execution;		//bin文件起始执行地址
}memory_desc;

void hex2bin_mount(memory_desc *mi);
void hex2bin_unmount(memory_desc *mi);
int hex2bin_read_hex(char *file_name, memory_desc *mi);
DWORD StringConvert(BYTE *pSrc, DWORD dwLen, BYTE *pOutStr, memory_desc *mi_fa, memory_desc *mi_fb);

#define MI_TOTAL_SIZE (sizeof(memory_desc) + MEMORY_MAX_BANK*MEMORY_BANK_SIZE)


#endif // __readhex_h__