.code
;RCX data
;RDX start index
;R8 data length
;R9 test

EncryptionASM proc

;ustawianie odpowiednich wartosci
add R8, RDX
add rcx, rdx

;zerowanie rejestru xmm2
pxor xmm2, xmm2

;ustawienie odpowiedniej maski
	mov RAX, 15
	pinsrb xmm2, RAX, 0  ;15
	dec RAX
	pinsrb xmm2,RAX, 4	 ;14
	dec RAX
	pinsrb xmm2,RAX, 8	 ;13
	dec RAX
	pinsrb xmm2,RAX, 12	 ;12
	dec RAX
	pinsrb xmm2,RAX, 1	 ;11
	dec RAX
	pinsrb xmm2,RAX, 5	 ;10
	dec RAX
	pinsrb xmm2,RAX, 9	 ;9
	dec RAX
	pinsrb xmm2,RAX, 13	 ;8
	dec RAX
	pinsrb xmm2,RAX, 2	 ;7
	dec RAX
	pinsrb xmm2,RAX, 6	 ;6
	dec RAX
	pinsrb xmm2,RAX, 10	 ;5
	dec RAX
	pinsrb xmm2,RAX, 14	 ;4
	dec RAX
	pinsrb xmm2,RAX, 3	 ;3
	dec RAX
	pinsrb xmm2,RAX, 7	 ;2
	dec RAX
	pinsrb xmm2,RAX, 11	 ;1
	dec RAX
	pinsrb xmm2,RAX, 15	 ;0

	;zerowanie rejestru xmm1
	pxor xmm1, xmm1
	;przesylanie wartosci z tablicy do xmm1
	movdqu xmm1, [RCX] 
	;zamiana wartosci w xmm1 wedlug maski podanej w xmm2
	PSHUFB XMM1, XMM2 
	;przesylanie wartosci z xmm1 do tablicy znakow
	movdqu [RCX],xmm1 
	ret

EncryptionASM endp
end