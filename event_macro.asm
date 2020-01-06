.macro s00
.byte 0x00
.endm

.macro s01
.byte 0x01
.endm

.macro s02
.byte 0x02
.endm

.macro s03
.byte 0x03
.endm

.macro s04 w
.byte 0x04
.word \w
.endm

.macro s05 w
.byte 0x05
.word \w
.endm

.macro s06 b w
.byte 0x06
.byte \b
.word \w
.endm

.macro s07 b w
.byte 0x07
.byte \b
.word \w
.endm

.macro s08 b
.byte 0x08
.byte \b
.endm

.macro s09 b
.byte 0x09
.byte \b
.endm

.macro s0a b1 b2
.byte 0x0a
.byte \b1,\b2
.endm

.macro s0b b1 b2
.byte 0x0b
.byte \b1,\b2
.endm

.macro s0c
.byte 0x0c
.endm

.macro s0d
.byte 0x0d
.endm

.macro s0e b
.byte 0x0e
.byte \b
.endm

.macro s0f b w
.byte 0x0f
.byte \b
.word \w
.endm

.macro s10 b1 b2
.byte 0x10
.byte \b1,\b2
.endm

.macro s11 b w
.byte 0x11
.byte \b
.word \w
.endm

.macro s12 b w
.byte 0x12
.byte \b
.word \w
.endm

.macro s13 b w
.byte 0x13
.byte \b
.word \w
.endm

.macro s14 b1 b2
.byte 0x14
.byte \b1,\b2
.endm

.macro s15 w1 w2
.byte 0x15
.word \w1,\w2
.endm

.macro s16 h1 h2
.byte 0x16
.hword \h1,\h2
.endm

.macro s17 h1 h2
.byte 0x17
.hword \h1,\h2
.endm

.macro s18 h1 h2
.byte 0x18
.hword \h1,\h2
.endm

.macro s19 h1 h2
.byte 0x19
.hword \h1,\h2
.endm

.macro s1a h1 h2
.byte 0x1a
.hword \h1,\h2
.endm

.macro s1b b1 b2
.byte 0x1b
.byte \b1,\b2
.endm

.macro s1c b1 b2
.byte 0x1c
.byte \b1,\b2
.endm

.macro s1d b w
.byte 0x1d
.byte \b
.word \w
.endm

.macro s1e w b
.byte 0x1e
.word \w
.byte \b
.endm

.macro s1f w b
.byte 0x1f
.word \w
.byte \b
.endm

.macro s20 w1 w2
.byte 0x20
.word \w1,\w2
.endm

.macro s21 h1 h2
.byte 0x21
.hword \h1,\h2
.endm

.macro s22 h1 h2
.byte 0x22
.hword \h1,\h2
.endm

.macro s23 w
.byte 0x23
.word \w+0x01
.endm

.macro s24 w
.byte 0x24
.word \w
.endm

.macro s25 h
.byte 0x25
.hword \h
.endm

.macro s26 h1 h2
.byte 0x26
.hword \h1,\h2
.endm

.macro s27
.byte 0x27
.endm

.macro s28 h
.byte 0x28
.hword \h
.endm

.macro s29 h
.byte 0x29
.hword \h
.endm

.macro s2a h
.byte 0x2a
.hword \h
.endm

.macro s2b h
.byte 0x2b
.hword \h
.endm

.macro s2c
.byte 0x2c
.endm

.macro s2d
.byte 0x2d
.endm

.macro s2e
.byte 0x2e
.endm

.macro s2f h
.byte 0x2f
.hword \h
.endm

.macro s30
.byte 0x30
.endm

.macro s31 h
.byte 0x31
.hword \h
.endm

.macro s32
.byte 0x32
.endm

.macro s33 h b
.byte 0x33
.hword \h
.byte \b
.endm

.macro s34 h
.byte 0x34
.hword \h
.endm

.macro s35
.byte 0x35
.endm

.macro s36 h
.byte 0x36
.hword \h
.endm

.macro s37 b
.byte 0x37
.byte \b
.endm

.macro s38 b
.byte 0x38
.byte \b
.endm

.macro s39 b1 b2 b3 h1 h2
.byte 0x39
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s3a b1 b2 b3 h1 h2
.byte 0x3a
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s3b b1 b2 b3 h1 h2
.byte 0x3b
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s3c b1 b2
.byte 0x3c
.byte \b1,\b2
.endm

.macro s3d b1 b2 b3 h1 h2
.byte 0x3d
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s3e b1 b2 b3 h1 h2
.byte 0x3e
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s3f b1 b2 b3 h1 h2
.byte 0x3f
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s40 b1 b2 b3 h1 h2
.byte 0x40
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s41 b1 b2 b3 h1 h2
.byte 0x41
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro s42 h1 h2
.byte 0x42
.hword \h1,\h2
.endm

.macro s43
.byte 0x43
.endm

.macro s44 h1 h2
.byte 0x44
.hword \h1,\h2
.endm

.macro s45 h1 h2
.byte 0x45
.hword \h1,\h2
.endm

.macro s46 h1 h2
.byte 0x46
.hword \h1,\h2
.endm

.macro s47 h1 h2
.byte 0x47
.hword \h1,\h2
.endm

.macro s48 h
.byte 0x48
.hword \h
.endm

.macro s49 h1 h2
.byte 0x49
.hword \h1,\h2
.endm

.macro s4a h1 h2
.byte 0x4a
.hword \h1,\h2
.endm

.macro s4b h
.byte 0x4b
.hword \h
.endm

.macro s4c h
.byte 0x4c
.hword \h
.endm

.macro s4d h
.byte 0x4d
.hword \h
.endm

.macro s4e h
.byte 0x4e
.hword \h
.endm

.macro s4f h w
.byte 0x4f
.hword \h
.word \w
.endm

.macro s50 h w b1 b2
.byte 0x50
.hword \h
.word \w
.byte \b1,\b2
.endm

.macro s51 h
.byte 0x51
.hword \h
.endm

.macro s52 h b1 b2
.byte 0x52
.hword \h
.byte \b1,\b2
.endm

.macro s53 h
.byte 0x53
.hword \h
.endm

.macro s54 h b1 b2
.byte 0x54
.hword \h
.byte \b1,\b2
.endm

.macro s55 h
.byte 0x55
.hword \h
.endm

.macro s56 h b1 b2
.byte 0x56
.hword \h
.byte \b1,\b2
.endm

.macro s57 h1 h2 h3
.byte 0x57
.hword \h1,\h2,\h3
.endm

.macro s58 h b1 b2
.byte 0x58
.hword \h
.byte \b1,\b2
.endm

.macro s59 h b1 b2
.byte 0x59
.hword \h
.byte \b1,\b2
.endm

.macro s5a
.byte 0x5a
.endm

.macro s5b h b
.byte 0x5b
.hword \h
.byte \b
.endm

.macro s5c_00 h w1 w2
.byte 0x5c,0x00
.hword \h,0x0000
.word \w1,\w2
.endm

.macro s5c_01 h w1 w2 w3
.byte 0x5c,0x01
.hword \h,0x0000
.word \w1,\w2,\w3
.endm

.macro s5c_02 h w1 w2 w3
.byte 0x5c,0x02
.hword \h,0x0000
.word \w1,\w2,\w3
.endm

.macro s5c_03 h w
.byte 0x5c,0x03
.hword \h,0x0000
.word \w
.endm

.macro s5c_04 h w1 w2 w3
.byte 0x5c,0x04
.hword \h,0x0000
.word \w1,\w2,\w3
.endm

.macro s5c_05 h w1 w2
.byte 0x5c,0x05
.hword \h,0x0000
.word \w1,\w2
.endm

.macro s5c_06 h w1 w2 w3 w4
.byte 0x5c,0x06
.hword \h,0x0000
.word \w1,\w2,\w3,\w4
.endm

.macro s5c_07 h w1 w2 w3
.byte 0x5c,0x07
.hword \h,0x0000
.word \w1,\w2,\w3
.endm

.macro s5c_08 h w1 w2 w3 w4
.byte 0x5c,0x08
.hword \h,0x0000
.word \w1,\w2,\w3,\w4
.endm

.macro s5c_09 h w1 w2
.byte 0x5c,0x09
.hword \h,0x0000
.word \w1,\w2
.endm

.macro s5d
.byte 0x5d
.endm

.macro s5e
.byte 0x5e
.endm

.macro s5f
.byte 0x5f
.endm

.macro s60 h
.byte 0x60
.hword \h
.endm

.macro s61 h
.byte 0x61
.hword \h
.endm

.macro s62 h
.byte 0x62
.hword \h
.endm

.macro s63 h1 h2 h3
.byte 0x63
.hword \h1,\h2,\h3
.endm

.macro s64 h
.byte 0x64
.hword \h
.endm

.macro s65 h b
.byte 0x65
.hword \h
.byte \b
.endm

.macro s66
.byte 0x66
.endm

.macro s67 w
.byte 0x67
.word \w
.endm

.macro s68
.byte 0x68
.endm

.macro s69
.byte 0x69
.endm

.macro s6a
.byte 0x6a
.endm

.macro s6b
.byte 0x6b
.endm

.macro s6c
.byte 0x6c
.endm

.macro s6d
.byte 0x6d
.endm

.macro s6e
.byte 0x6e,0x00,0x00
.endm

.macro s6f b1 b2 b3 b4
.byte 0x6f
.byte \b1,\b2,\b3,\b4
.endm

.macro s70 b1 b2 b3 b4 b5
.byte 0x70
.byte \b1,\b2,\b3,\b4,\b5
.endm

.macro s71 b1 b2 b3 b4 b5
.byte 0x71
.byte \b1,\b2,\b3,\b4,\b5
.endm

.macro s72
.byte 0x72
.endm

.macro s73
.byte 0x73
.endm

.macro s74
.byte 0x74
.endm

.macro s75 h b1 b2
.byte 0x75
.hword \h
.byte \b1,\b2
.endm

.macro s76
.byte 0x76
.endm

.macro s77 b
.byte 0x77
.byte \b
.endm

.macro s78 w
.byte 0x78
.word \w
.endm

.macro s79 h1 b h2
.byte 0x79
.hword \h1
.byte \b
.hword \h2
.byte 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
.endm

.macro s7a h
.byte 0x7a
.hword \h
.endm

.macro s7b b1 b2 h
.byte 0x7b
.byte \b1,\b2
.hword \h
.endm

.macro s7c h
.byte 0x7c
.hword \h
.endm

.macro s7d b h
.byte 0x7d
.byte \b
.hword \h
.endm

.macro s7e b
.byte 0x7e
.byte \b
.endm

.macro s7f b h
.byte 0x7f
.byte \b
.hword \h
.endm

.macro s80 b h
.byte 0x80
.byte \b
.hword \h
.endm

.macro s81 b h
.byte 0x81
.byte \b
.hword \h
.endm

.macro s82 b h
.byte 0x82
.byte \b
.hword \h
.endm

.macro s83 b h
.byte 0x83
.byte \b
.hword \h
.endm

.macro s84 b h
.byte 0x84
.byte \b
.hword \h
.endm

.macro s85 b w
.byte 0x85
.byte \b
.word \w
.endm

.macro s86 w
.byte 0x86
.word \w
.endm

.macro s87 w
.byte 0x87
.word \w
.endm

.macro s88 w
.byte 0x88
.word \w
.endm

.macro s89 h
.byte 0x89
.hword \h
.endm

.macro s8a
.byte 0x8a
.endm

.macro s8b
.byte 0x8b
.endm

.macro s8c
.byte 0x8c
.endm

.macro s8d
.byte 0x8d
.endm

.macro s8e
.byte 0x8e
.endm

.macro s8f h
.byte 0x8f
.hword \h
.endm

.macro s90 w
.byte 0x90
.word \w
.byte 0x00
.endm

.macro s91 w
.byte 0x91
.word \w
.byte 0x00
.endm

.macro s92 w
.byte 0x92
.word \w
.byte 0x00
.endm

.macro s93 b1 b2
.byte 0x93
.byte \b1,\b2,0x00
.endm

.macro s94
.byte 0x94
.endm

.macro s95 b1 b2
.byte 0x95
.byte 0x00,0x00,0x00
.endm

.macro s96
.byte 0x96
.endm

.macro s97 b
.byte 0x97
.byte \b
.endm

.macro s98 b1 b2
.byte 0x98
.byte \b1,\b2
.endm

.macro s99 h
.byte 0x99
.hword \h
.endm

.macro s9a b
.byte 0x9a
.byte \b
.endm

.macro s9b w
.byte 0x9b
.word \w
.endm

.macro s9c h
.byte 0x9c
.hword \h
.endm

.macro s9d b h
.byte 0x9d
.byte \b
.hword \h
.endm

.macro s9e h
.byte 0x9e
.hword \h
.endm

.macro s9f h
.byte 0x9f
.hword \h
.endm

.macro sa0
.byte 0xa0
.endm

.macro sa1 h1 h2
.byte 0xa1
.hword \h1,\h2
.endm

.macro sa2 h1 h2 h3 h4
.byte 0xa2
.hword \h1,\h2,\h3,\h4
.endm

.macro sa3
.byte 0xa3
.endm

.macro sa4 h
.byte 0xa4
.hword \h
.endm

.macro sa5
.byte 0xa5
.endm

.macro sa6
.byte 0xa6
.byte 0x04
.endm

.macro sa7 h
.byte 0xa7
.hword \h
.endm

.macro sa8 h b1 b2
.byte 0xa8
.hword \h
.byte \b1,\b2
.byte 0x00
.endm

.macro sa9 h b1 b2
.byte 0xa9
.hword \h
.byte \b1,\b2
.endm

.macro saa b1 b2 h1 h2 b3 b4
.byte 0xaa
.byte \b1,\b2
.hword \h1,\h2
.byte \b3,\b4
.endm

.macro sab b1 b2
.byte 0xab
.byte \b1,\b2
.endm

.macro sac h1 h2
.byte 0xac
.hword \h1,\h2
.endm

.macro sad h1 h2
.byte 0xad
.hword \h1,\h2
.endm

.macro sae
.byte 0xae
.endm

.macro saf h1 h2
.byte 0xaf
.hword \h1,\h2
.endm

.macro sb0 h1 h2
.byte 0xb0
.hword \h1,\h2
.endm

.macro sb1
.byte 0xb1
.endm

.macro sb2
.byte 0xb2
.endm

.macro sb3 h
.byte 0xb3
.hword \h
.endm

.macro sb4 h
.byte 0xb4
.hword \h
.endm

.macro sb5 h
.byte 0xb5
.hword \h
.endm

.macro sb6 h1 b h2
.byte 0xb6
.hword \h1
.byte \b
.hword \h2
.endm

.macro sb7
.byte 0xb7
.endm

.macro sb8 w
.byte 0xb8
.word \w
.endm

.macro sb9 w
.byte 0xb9
.word \w
.endm

.macro sba w
.byte 0xba
.word \w
.endm

.macro sbb b w
.byte 0xbb
.byte \b
.word \w
.endm

.macro sbc b w
.byte 0xbc
.byte \b
.word \w
.endm

.macro sbd w
.byte 0xbd
.word \w
.endm

.macro sbe w
.byte 0xbe
.word \w
.endm

.macro sbf b w
.byte 0xbf
.byte \b
.word \w
.endm

.macro sc0 b1 b2
.byte 0xc0
.byte \b1,\b2
.endm

.macro sc1 b1 b2
.byte 0xc1
.byte \b1,\b2
.endm

.macro sc2 b1 b2
.byte 0xc2
.byte \b1,\b2
.endm

.macro sc3 b
.byte 0xc3
.byte \b
.endm

.macro sc4 b1 b2 b3 h1 h2
.byte 0xc4
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro sc5
.byte 0xc5
.endm

.macro sc6 b h
.byte 0xc6
.byte \b
.hword \h
.endm

.macro sc7 b
.byte 0xc7
.byte \b
.endm

.macro sc8 w
.byte 0xc8
.word \w
.endm

.macro sc9
.byte 0xc9
.endm

.macro sca
.byte 0xca
.endm

.macro scb
.byte 0xcb
.endm

.macro scc b w
.byte 0xcc
.byte \b
.word \w
.endm

.macro scd h
.byte 0xcd
.hword \h
.endm

.macro sce h
.byte 0xce
.hword \h
.endm

.macro scf
.byte 0xcf
.endm

.macro sd0 h
.byte 0xd0
.hword \h
.endm

.macro sd1 b1 b2 b3 h1 h2
.byte 0xd1
.byte \b1,\b2,\b3
.hword \h1,\h2
.endm

.macro sd2 h b
.byte 0xd2
.hword \h
.byte \b
.endm

.macro sd3
.byte 0xd3
.endm

.macro sd4
.byte 0xd4
.endm

.macro sd5
.byte 0xd5
.endm

.macro text w b
.byte 0x0f,0x00
.word \w
.byte 0x09,\b
.endm
