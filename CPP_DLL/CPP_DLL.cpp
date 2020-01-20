#include "stdafx.h"
#include "nmmintrin.h" // for SSE4.2


extern "C"  void  __declspec(dllexport)  EncryptionCPP(unsigned char *data, int dataStart)
{
	//Ustawianie maski w rejestrze
	__m128i mask = _mm_setzero_si128();
	mask = _mm_insert_epi8(mask, 15, 0);
	mask = _mm_insert_epi8(mask, 14, 4);
	mask = _mm_insert_epi8(mask, 13, 8);
	mask = _mm_insert_epi8(mask, 12, 12);
	mask = _mm_insert_epi8(mask, 11, 1);
	mask = _mm_insert_epi8(mask, 10, 5);
	mask = _mm_insert_epi8(mask, 9, 9);
	mask = _mm_insert_epi8(mask, 8, 13);
	mask = _mm_insert_epi8(mask, 7, 2);
	mask = _mm_insert_epi8(mask, 6, 6);
	mask = _mm_insert_epi8(mask, 5, 10);
	mask = _mm_insert_epi8(mask, 4, 14);
	mask = _mm_insert_epi8(mask, 3, 3);
	mask = _mm_insert_epi8(mask, 2, 7);
	mask = _mm_insert_epi8(mask, 1, 11);
	mask = _mm_insert_epi8(mask, 0, 15);

	//Zerowanie rejestru na dane
	__m128i dataRegister = _mm_setzero_si128();

	//Za³adowanie do rejestru danych z tablicy znaków
	dataRegister = _mm_loadu_si128((__m128i*)(data + dataStart));

	//Szyfrowanie danych przy pomocy rejestru maski oraz rejestru danych
	__m128i result = _mm_shuffle_epi8(dataRegister, mask);

	//Za³adowanie danych z rejestru do tablicy znaków
	_mm_storeu_si128((__m128i*)(data + dataStart), result);
}