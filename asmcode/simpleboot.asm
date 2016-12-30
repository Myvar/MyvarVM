hang:
    jmp hang
 
    times 510-($-$$) db 0 ; 2 bytes less now
    db 0x55
    db 0xAA
