﻿using System.Security.Cryptography;
using SecureRemotePassword;
using Xunit;

namespace P3.Driver.HomeKit.UnitTests.Crypto
{
    public class SrpTests
    {
        private static readonly byte[] SrpSalt =
        {
            0xBE, 0xB2, 0x53, 0x79, 0xD1, 0xA8, 0x58, 0x1E,
            0xB5, 0xA7, 0x27, 0x67, 0x3A, 0x24, 0x41, 0xEE
        };

        private static readonly string SrpUser = "alice";
        private static readonly string SrpPass = "password123";

        private static readonly byte[] SrpV =
        {
            0x9b, 0x5e, 0x06, 0x17, 0x01, 0xea, 0x7a, 0xeb, 0x39, 0xcf, 0x6e, 0x35, 0x19, 0x65, 0x5a, 0x85, 0x3c, 0xf9,
            0x4c,
            0x75, 0xca, 0xf2, 0x55, 0x5e, 0xf1, 0xfa, 0xf7, 0x59, 0xbb, 0x79, 0xcb, 0x47, 0x70, 0x14, 0xe0, 0x4a, 0x88,
            0xd6,
            0x8f, 0xfc, 0x05, 0x32, 0x38, 0x91, 0xd4, 0xc2, 0x05, 0xb8, 0xde, 0x81, 0xc2, 0xf2, 0x03, 0xd8, 0xfa, 0xd1,
            0xb2,
            0x4d, 0x2c, 0x10, 0x97, 0x37, 0xf1, 0xbe, 0xbb, 0xd7, 0x1f, 0x91, 0x24, 0x47, 0xc4, 0xa0, 0x3c, 0x26, 0xb9,
            0xfa,
            0xd8, 0xed, 0xb3, 0xe7, 0x80, 0x77, 0x8e, 0x30, 0x25, 0x29, 0xed, 0x1e, 0xe1, 0x38, 0xcc, 0xfc, 0x36, 0xd4,
            0xba,
            0x31, 0x3c, 0xc4, 0x8b, 0x14, 0xea, 0x8c, 0x22, 0xa0, 0x18, 0x6b, 0x22, 0x2e, 0x65, 0x5f, 0x2d, 0xf5, 0x60,
            0x3f,
            0xd7, 0x5d, 0xf7, 0x6b, 0x3b, 0x08, 0xff, 0x89, 0x50, 0x06, 0x9a, 0xdd, 0x03, 0xa7, 0x54, 0xee, 0x4a, 0xe8,
            0x85,
            0x87, 0xcc, 0xe1, 0xbf, 0xde, 0x36, 0x79, 0x4d, 0xba, 0xe4, 0x59, 0x2b, 0x7b, 0x90, 0x4f, 0x44, 0x2b, 0x04,
            0x1c,
            0xb1, 0x7a, 0xeb, 0xad, 0x1e, 0x3a, 0xeb, 0xe3, 0xcb, 0xe9, 0x9d, 0xe6, 0x5f, 0x4b, 0xb1, 0xfa, 0x00, 0xb0,
            0xe7,
            0xaf, 0x06, 0x86, 0x3d, 0xb5, 0x3b, 0x02, 0x25, 0x4e, 0xc6, 0x6e, 0x78, 0x1e, 0x3b, 0x62, 0xa8, 0x21, 0x2c,
            0x86,
            0xbe, 0xb0, 0xd5, 0x0b, 0x5b, 0xa6, 0xd0, 0xb4, 0x78, 0xd8, 0xc4, 0xe9, 0xbb, 0xce, 0xc2, 0x17, 0x65, 0x32,
            0x6f,
            0xbd, 0x14, 0x05, 0x8d, 0x2b, 0xbd, 0xe2, 0xc3, 0x30, 0x45, 0xf0, 0x38, 0x73, 0xe5, 0x39, 0x48, 0xd7, 0x8b,
            0x79,
            0x4f, 0x07, 0x90, 0xe4, 0x8c, 0x36, 0xae, 0xd6, 0xe8, 0x80, 0xf5, 0x57, 0x42, 0x7b, 0x2f, 0xc0, 0x6d, 0xb5,
            0xe1,
            0xe2, 0xe1, 0xd7, 0xe6, 0x61, 0xac, 0x48, 0x2d, 0x18, 0xe5, 0x28, 0xd7, 0x29, 0x5e, 0xf7, 0x43, 0x72, 0x95,
            0xff,
            0x1a, 0x72, 0xd4, 0x02, 0x77, 0x17, 0x13, 0xf1, 0x68, 0x76, 0xdd, 0x05, 0x0a, 0xe5, 0xb7, 0xad, 0x53, 0xcc,
            0xb9,
            0x08, 0x55, 0xc9, 0x39, 0x56, 0x64, 0x83, 0x58, 0xad, 0xfd, 0x96, 0x64, 0x22, 0xf5, 0x24, 0x98, 0x73, 0x2d,
            0x68,
            0xd1, 0xd7, 0xfb, 0xef, 0x10, 0xd7, 0x80, 0x34, 0xab, 0x8d, 0xcb, 0x6f, 0x0f, 0xcf, 0x88, 0x5c, 0xc2, 0xb2,
            0xea,
            0x2c, 0x3e, 0x6a, 0xc8, 0x66, 0x09, 0xea, 0x05, 0x8a, 0x9d, 0xa8, 0xcc, 0x63, 0x53, 0x1d, 0xc9, 0x15, 0x41,
            0x4d,
            0xf5, 0x68, 0xb0, 0x94, 0x82, 0xdd, 0xac, 0x19, 0x54, 0xde, 0xc7, 0xeb, 0x71, 0x4f, 0x6f, 0xf7, 0xd4, 0x4c,
            0xd5,
            0xb8, 0x6f, 0x6b, 0xd1, 0x15, 0x81, 0x09, 0x30, 0x63, 0x7c, 0x01, 0xd0, 0xf6, 0x01, 0x3b, 0xc9, 0x74, 0x0f,
            0xa2,
            0xc6, 0x33, 0xba, 0x89,
        };

        private static readonly byte[] SrpBPrivate =
        {
            0xe4, 0x87, 0xcb, 0x59, 0xd3, 0x1a, 0xc5, 0x50, 0x47, 0x1e, 0x81, 0xf0, 0x0f, 0x69, 0x28, 0xe0,
            0x1d, 0xda, 0x08, 0xe9, 0x74, 0xa0, 0x04, 0xf4, 0x9e, 0x61, 0xf5, 0xd1, 0x05, 0x28, 0x4d, 0x20,
        };

        private static readonly byte[] SrpAPublic =
        {
            0xfa, 0xb6, 0xf5, 0xd2, 0x61, 0x5d, 0x1e, 0x32, 0x35, 0x12, 0xe7, 0x99, 0x1c, 0xc3, 0x74, 0x43, 0xf4, 0x87,
            0xda,
            0x60, 0x4c, 0xa8, 0xc9, 0x23, 0x0f, 0xcb, 0x04, 0xe5, 0x41, 0xdc, 0xe6, 0x28, 0x0b, 0x27, 0xca, 0x46, 0x80,
            0xb0,
            0x37, 0x4f, 0x17, 0x9d, 0xc3, 0xbd, 0xc7, 0x55, 0x3f, 0xe6, 0x24, 0x59, 0x79, 0x8c, 0x70, 0x1a, 0xd8, 0x64,
            0xa9,
            0x13, 0x90, 0xa2, 0x8c, 0x93, 0xb6, 0x44, 0xad, 0xbf, 0x9c, 0x00, 0x74, 0x5b, 0x94, 0x2b, 0x79, 0xf9, 0x01,
            0x2a,
            0x21, 0xb9, 0xb7, 0x87, 0x82, 0x31, 0x9d, 0x83, 0xa1, 0xf8, 0x36, 0x28, 0x66, 0xfb, 0xd6, 0xf4, 0x6b, 0xfc,
            0x0d,
            0xdb, 0x2e, 0x1a, 0xb6, 0xe4, 0xb4, 0x5a, 0x99, 0x06, 0xb8, 0x2e, 0x37, 0xf0, 0x5d, 0x6f, 0x97, 0xf6, 0xa3,
            0xeb,
            0x6e, 0x18, 0x20, 0x79, 0x75, 0x9c, 0x4f, 0x68, 0x47, 0x83, 0x7b, 0x62, 0x32, 0x1a, 0xc1, 0xb4, 0xfa, 0x68,
            0x64,
            0x1f, 0xcb, 0x4b, 0xb9, 0x8d, 0xd6, 0x97, 0xa0, 0xc7, 0x36, 0x41, 0x38, 0x5f, 0x4b, 0xab, 0x25, 0xb7, 0x93,
            0x58,
            0x4c, 0xc3, 0x9f, 0xc8, 0xd4, 0x8d, 0x4b, 0xd8, 0x67, 0xa9, 0xa3, 0xc1, 0x0f, 0x8e, 0xa1, 0x21, 0x70, 0x26,
            0x8e,
            0x34, 0xfe, 0x3b, 0xbe, 0x6f, 0xf8, 0x99, 0x98, 0xd6, 0x0d, 0xa2, 0xf3, 0xe4, 0x28, 0x3c, 0xbe, 0xc1, 0x39,
            0x3d,
            0x52, 0xaf, 0x72, 0x4a, 0x57, 0x23, 0x0c, 0x60, 0x4e, 0x9f, 0xbc, 0xe5, 0x83, 0xd7, 0x61, 0x3e, 0x6b, 0xff,
            0xd6,
            0x75, 0x96, 0xad, 0x12, 0x1a, 0x87, 0x07, 0xee, 0xc4, 0x69, 0x44, 0x95, 0x70, 0x33, 0x68, 0x6a, 0x15, 0x5f,
            0x64,
            0x4d, 0x5c, 0x58, 0x63, 0xb4, 0x8f, 0x61, 0xbd, 0xbf, 0x19, 0xa5, 0x3e, 0xab, 0x6d, 0xad, 0x0a, 0x18, 0x6b,
            0x8c,
            0x15, 0x2e, 0x5f, 0x5d, 0x8c, 0xad, 0x4b, 0x0e, 0xf8, 0xaa, 0x4e, 0xa5, 0x00, 0x88, 0x34, 0xc3, 0xcd, 0x34,
            0x2e,
            0x5e, 0x0f, 0x16, 0x7a, 0xd0, 0x45, 0x92, 0xcd, 0x8b, 0xd2, 0x79, 0x63, 0x93, 0x98, 0xef, 0x9e, 0x11, 0x4d,
            0xfa,
            0xaa, 0xb9, 0x19, 0xe1, 0x4e, 0x85, 0x09, 0x89, 0x22, 0x4d, 0xdd, 0x98, 0x57, 0x6d, 0x79, 0x38, 0x5d, 0x22,
            0x10,
            0x90, 0x2e, 0x9f, 0x9b, 0x1f, 0x2d, 0x86, 0xcf, 0xa4, 0x7e, 0xe2, 0x44, 0x63, 0x54, 0x65, 0xf7, 0x10, 0x58,
            0x42,
            0x1a, 0x01, 0x84, 0xbe, 0x51, 0xdd, 0x10, 0xcc, 0x9d, 0x07, 0x9e, 0x6f, 0x16, 0x04, 0xe7, 0xaa, 0x9b, 0x7c,
            0xf7,
            0x88, 0x3c, 0x7d, 0x4c, 0xe1, 0x2b, 0x06, 0xeb, 0xe1, 0x60, 0x81, 0xe2, 0x3f, 0x27, 0xa2, 0x31, 0xd1, 0x84,
            0x32,
            0xd7, 0xd1, 0xbb, 0x55, 0xc2, 0x8a, 0xe2, 0x1f, 0xfc, 0xf0, 0x05, 0xf5, 0x75, 0x28, 0xd1, 0x5a, 0x88, 0x88,
            0x1b,
            0xb3, 0xbb, 0xb7, 0xfe,
        };

        private static readonly byte[] SrpAPrivate =
        {
            0x60, 0x97, 0x55, 0x27, 0x03, 0x5C, 0xF2, 0xAD, 0x19, 0x89, 0x80, 0x6F, 0x04, 0x07, 0x21, 0x0B, 0xC8, 0x1E,
            0xDC, 0x04, 0xE2, 0x76, 0x2A, 0x56, 0xAF, 0xD5, 0x29, 0xDD, 0xDA, 0x2D, 0x43, 0x93
        };

        private static readonly byte[] SrpBPublic =
        {
            0x40, 0xf5, 0x70, 0x88, 0xa4, 0x82, 0xd4, 0xc7, 0x73, 0x33, 0x84, 0xfe, 0x0d, 0x30, 0x1f, 0xdd, 0xca, 0x90,
            0x80,
            0xad, 0x7d, 0x4f, 0x6f, 0xdf, 0x09, 0xa0, 0x10, 0x06, 0xc3, 0xcb, 0x6d, 0x56, 0x2e, 0x41, 0x63, 0x9a, 0xe8,
            0xfa,
            0x21, 0xde, 0x3b, 0x5d, 0xba, 0x75, 0x85, 0xb2, 0x75, 0x58, 0x9b, 0xdb, 0x27, 0x98, 0x63, 0xc5, 0x62, 0x80,
            0x7b,
            0x2b, 0x99, 0x08, 0x3c, 0xd1, 0x42, 0x9c, 0xdb, 0xe8, 0x9e, 0x25, 0xbf, 0xbd, 0x7e, 0x3c, 0xad, 0x31, 0x73,
            0xb2,
            0xe3, 0xc5, 0xa0, 0xb1, 0x74, 0xda, 0x6d, 0x53, 0x91, 0xe6, 0xa0, 0x6e, 0x46, 0x5f, 0x03, 0x7a, 0x40, 0x06,
            0x25,
            0x48, 0x39, 0xa5, 0x6b, 0xf7, 0x6d, 0xa8, 0x4b, 0x1c, 0x94, 0xe0, 0xae, 0x20, 0x85, 0x76, 0x15, 0x6f, 0xe5,
            0xc1,
            0x40, 0xa4, 0xba, 0x4f, 0xfc, 0x9e, 0x38, 0xc3, 0xb0, 0x7b, 0x88, 0x84, 0x5f, 0xc6, 0xf7, 0xdd, 0xda, 0x93,
            0x38,
            0x1f, 0xe0, 0xca, 0x60, 0x84, 0xc4, 0xcd, 0x2d, 0x33, 0x6e, 0x54, 0x51, 0xc4, 0x64, 0xcc, 0xb6, 0xec, 0x65,
            0xe7,
            0xd1, 0x6e, 0x54, 0x8a, 0x27, 0x3e, 0x82, 0x62, 0x84, 0xaf, 0x25, 0x59, 0xb6, 0x26, 0x42, 0x74, 0x21, 0x59,
            0x60,
            0xff, 0xf4, 0x7b, 0xdd, 0x63, 0xd3, 0xaf, 0xf0, 0x64, 0xd6, 0x13, 0x7a, 0xf7, 0x69, 0x66, 0x1c, 0x9d, 0x4f,
            0xee,
            0x47, 0x38, 0x26, 0x03, 0xc8, 0x8e, 0xaa, 0x09, 0x80, 0x58, 0x1d, 0x07, 0x75, 0x84, 0x61, 0xb7, 0x77, 0xe4,
            0x35,
            0x6d, 0xda, 0x58, 0x35, 0x19, 0x8b, 0x51, 0xfe, 0xea, 0x30, 0x8d, 0x70, 0xf7, 0x54, 0x50, 0xb7, 0x16, 0x75,
            0xc0,
            0x8c, 0x7d, 0x83, 0x02, 0xfd, 0x75, 0x39, 0xdd, 0x1f, 0xf2, 0xa1, 0x1c, 0xb4, 0x25, 0x8a, 0xa7, 0x0d, 0x23,
            0x44,
            0x36, 0xaa, 0x42, 0xb6, 0xa0, 0x61, 0x5f, 0x3f, 0x91, 0x5d, 0x55, 0xcc, 0x3b, 0x96, 0x6b, 0x27, 0x16, 0xb3,
            0x6e,
            0x4d, 0x1a, 0x06, 0xce, 0x5e, 0x5d, 0x2e, 0xa3, 0xbe, 0xe5, 0xa1, 0x27, 0x0e, 0x87, 0x51, 0xda, 0x45, 0xb6,
            0x0b,
            0x99, 0x7b, 0x0f, 0xfd, 0xb0, 0xf9, 0x96, 0x2f, 0xee, 0x4f, 0x03, 0xbe, 0xe7, 0x80, 0xba, 0x0a, 0x84, 0x5b,
            0x1d,
            0x92, 0x71, 0x42, 0x17, 0x83, 0xae, 0x66, 0x01, 0xa6, 0x1e, 0xa2, 0xe3, 0x42, 0xe4, 0xf2, 0xe8, 0xbc, 0x93,
            0x5a,
            0x40, 0x9e, 0xad, 0x19, 0xf2, 0x21, 0xbd, 0x1b, 0x74, 0xe2, 0x96, 0x4d, 0xd1, 0x9f, 0xc8, 0x45, 0xf6, 0x0e,
            0xfc,
            0x09, 0x33, 0x8b, 0x60, 0xb6, 0xb2, 0x56, 0xd8, 0xca, 0xc8, 0x89, 0xcc, 0xa3, 0x06, 0xcc, 0x37, 0x0a, 0x0b,
            0x18,
            0xc8, 0xb8, 0x86, 0xe9, 0x5d, 0xa0, 0xaf, 0x52, 0x35, 0xfe, 0xf4, 0x39, 0x30, 0x20, 0xd2, 0xb7, 0xf3, 0x05,
            0x69,
            0x04, 0x75, 0x90, 0x42,
        };

        private static readonly byte[] SrpU =
        {
            0x03, 0xae, 0x5f, 0x3c, 0x3f, 0xa9, 0xef, 0xf1, 0xa5, 0x0d, 0x7d, 0xbb, 0x8d, 0x2f, 0x60, 0xa1,
            0xea, 0x66, 0xea, 0x71, 0x2d, 0x50, 0xae, 0x97, 0x6e, 0xe3, 0x46, 0x41, 0xa1, 0xcd, 0x0e, 0x51,
            0xc4, 0x68, 0x3d, 0xa3, 0x83, 0xe8, 0x59, 0x5d, 0x6c, 0xb5, 0x6a, 0x15, 0xd5, 0xfb, 0xc7, 0x54,
            0x3e, 0x07, 0xfb, 0xdd, 0xd3, 0x16, 0x21, 0x7e, 0x01, 0xa3, 0x91, 0xa1, 0x8e, 0xf0, 0x6d, 0xff,
        };

        private static readonly byte[] SrpS =
        {
            0xf1, 0x03, 0x6f, 0xec, 0xd0, 0x17, 0xc8, 0x23, 0x9c, 0x0d, 0x5a, 0xf7, 0xe0, 0xfc, 0xf0, 0xd4, 0x08, 0xb0,
            0x09,
            0xe3, 0x64, 0x11, 0x61, 0x8a, 0x60, 0xb2, 0x3a, 0xab, 0xbf, 0xc3, 0x83, 0x39, 0x72, 0x68, 0x23, 0x12, 0x14,
            0xba,
            0xac, 0xdc, 0x94, 0xca, 0x1c, 0x53, 0xf4, 0x42, 0xfb, 0x51, 0xc1, 0xb0, 0x27, 0xc3, 0x18, 0xae, 0x23, 0x8e,
            0x16,
            0x41, 0x4d, 0x60, 0xd1, 0x88, 0x1b, 0x66, 0x48, 0x6a, 0xde, 0x10, 0xed, 0x02, 0xba, 0x33, 0xd0, 0x98, 0xf6,
            0xce,
            0x9b, 0xcf, 0x1b, 0xb0, 0xc4, 0x6c, 0xa2, 0xc4, 0x7f, 0x2f, 0x17, 0x4c, 0x59, 0xa9, 0xc6, 0x1e, 0x25, 0x60,
            0x89,
            0x9b, 0x83, 0xef, 0x61, 0x13, 0x1e, 0x6f, 0xb3, 0x0b, 0x71, 0x4f, 0x4e, 0x43, 0xb7, 0x35, 0xc9, 0xfe, 0x60,
            0x80,
            0x47, 0x7c, 0x1b, 0x83, 0xe4, 0x09, 0x3e, 0x4d, 0x45, 0x6b, 0x9b, 0xca, 0x49, 0x2c, 0xf9, 0x33, 0x9d, 0x45,
            0xbc,
            0x42, 0xe6, 0x7c, 0xe6, 0xc0, 0x2c, 0x24, 0x3e, 0x49, 0xf5, 0xda, 0x42, 0xa8, 0x69, 0xec, 0x85, 0x57, 0x80,
            0xe8,
            0x42, 0x07, 0xb8, 0xa1, 0xea, 0x65, 0x01, 0xc4, 0x78, 0xaa, 0xc0, 0xdf, 0xd3, 0xd2, 0x26, 0x14, 0xf5, 0x31,
            0xa0,
            0x0d, 0x82, 0x6b, 0x79, 0x54, 0xae, 0x8b, 0x14, 0xa9, 0x85, 0xa4, 0x29, 0x31, 0x5e, 0x6d, 0xd3, 0x66, 0x4c,
            0xf4,
            0x71, 0x81, 0x49, 0x6a, 0x94, 0x32, 0x9c, 0xde, 0x80, 0x05, 0xca, 0xe6, 0x3c, 0x2f, 0x9c, 0xa4, 0x96, 0x9b,
            0xfe,
            0x84, 0x00, 0x19, 0x24, 0x03, 0x7c, 0x44, 0x65, 0x59, 0xbd, 0xbb, 0x9d, 0xb9, 0xd4, 0xdd, 0x14, 0x2f, 0xbc,
            0xd7,
            0x5e, 0xef, 0x2e, 0x16, 0x2c, 0x84, 0x30, 0x65, 0xd9, 0x9e, 0x8f, 0x05, 0x76, 0x2c, 0x4d, 0xb7, 0xab, 0xd9,
            0xdb,
            0x20, 0x3d, 0x41, 0xac, 0x85, 0xa5, 0x8c, 0x05, 0xbd, 0x4e, 0x2d, 0xbf, 0x82, 0x2a, 0x93, 0x45, 0x23, 0xd5,
            0x4e,
            0x06, 0x53, 0xd3, 0x76, 0xce, 0x8b, 0x56, 0xdc, 0xb4, 0x52, 0x7d, 0xdd, 0xc1, 0xb9, 0x94, 0xdc, 0x75, 0x09,
            0x46,
            0x3a, 0x74, 0x68, 0xd7, 0xf0, 0x2b, 0x1b, 0xeb, 0x16, 0x85, 0x71, 0x4c, 0xe1, 0xdd, 0x1e, 0x71, 0x80, 0x8a,
            0x13,
            0x7f, 0x78, 0x88, 0x47, 0xb7, 0xc6, 0xb7, 0xbf, 0xa1, 0x36, 0x44, 0x74, 0xb3, 0xb7, 0xe8, 0x94, 0x78, 0x95,
            0x4f,
            0x6a, 0x8e, 0x68, 0xd4, 0x5b, 0x85, 0xa8, 0x8e, 0x4e, 0xbf, 0xec, 0x13, 0x36, 0x8e, 0xc0, 0x89, 0x1c, 0x3b,
            0xc8,
            0x6c, 0xf5, 0x00, 0x97, 0x88, 0x01, 0x78, 0xd8, 0x61, 0x35, 0xe7, 0x28, 0x72, 0x34, 0x58, 0x53, 0x88, 0x58,
            0xd7,
            0x15, 0xb7, 0xb2, 0x47, 0x40, 0x62, 0x22, 0xc1, 0x01, 0x9f, 0x53, 0x60, 0x3f, 0x01, 0x69, 0x52, 0xd4, 0x97,
            0x10,
            0x08, 0x58, 0x82, 0x4c,
        };

        private static readonly byte[] SrpK =
        {
            0x5c, 0xbc, 0x21, 0x9d, 0xb0, 0x52, 0x13, 0x8e, 0xe1, 0x14, 0x8c, 0x71, 0xcd, 0x44, 0x98, 0x96,
            0x3d, 0x68, 0x25, 0x49, 0xce, 0x91, 0xca, 0x24, 0xf0, 0x98, 0x46, 0x8f, 0x06, 0x01, 0x5b, 0xeb,
            0x6a, 0xf2, 0x45, 0xc2, 0x09, 0x3f, 0x98, 0xc3, 0x65, 0x1b, 0xca, 0x83, 0xab, 0x8c, 0xab, 0x2b,
            0x58, 0x0b, 0xbf, 0x02, 0x18, 0x4f, 0xef, 0xdf, 0x26, 0x14, 0x2f, 0x73, 0xdf, 0x95, 0xac, 0x50,
        };

        private static readonly byte[] SrpM1 =
        {
            0x5f, 0x7c, 0x14, 0xab, 0x57, 0xed, 0x0e, 0x94, 0xfd, 0x1d, 0x78, 0xc6, 0xb4, 0xdd, 0x09, 0xed,
            0x7e, 0x34, 0x0b, 0x7e, 0x05, 0xd4, 0x19, 0xa9, 0xfd, 0x76, 0x0f, 0x6b, 0x35, 0xe5, 0x23, 0xd1,
            0x31, 0x07, 0x77, 0xa1, 0xae, 0x1d, 0x28, 0x26, 0xf5, 0x96, 0xf3, 0xa8, 0x51, 0x16, 0xcc, 0x45,
            0x7c, 0x7c, 0x96, 0x4d, 0x4f, 0x44, 0xde, 0xd5, 0x55, 0x9d, 0xa8, 0x18, 0xc8, 0x8b, 0x61, 0x7f,
        };

        private static readonly byte[] SrpM2 =
        {
            0x2f, 0xa0, 0xe8, 0x1f, 0x5c, 0xb7, 0x3b, 0x88, 0xfa, 0x09, 0x64, 0x27, 0x0f, 0x32, 0x1d, 0xd6,
            0x41, 0xf2, 0x22, 0x7a, 0x5d, 0x80, 0x5c, 0x40, 0xf1, 0xbf, 0xe9, 0x6a, 0xaf, 0x6a, 0x19, 0xff,
            0xce, 0x8e, 0x23, 0x28, 0x79, 0x65, 0xa3, 0x9e, 0xab, 0x9d, 0x5a, 0x02, 0x21, 0x5f, 0x89, 0xe1,
            0x28, 0x17, 0x7e, 0xd2, 0xc4, 0xf1, 0x03, 0xe6, 0x55, 0xa0, 0x45, 0x53, 0x1b, 0xcb, 0xf7, 0xad,
        };

        private readonly SrpInteger _saltInt;
        private readonly SrpClient _srpClient;

        public SrpTests()
        {
            var customParams = SrpParameters.Create3072<SHA512>();
            _saltInt = SrpInteger.FromByteArray(SrpSalt);
            _srpClient = new SrpClient(customParams);
        }

        [Fact]
        public void TestSrpVerifier()
        {
            var privateKey = _srpClient.DerivePrivateKey(_saltInt.ToHex(), SrpUser, SrpPass);
            var verifier = _srpClient.DeriveVerifier(privateKey);

            var clientEp = new SrpEphemeral
            {
                Secret = SrpInteger.FromByteArray(SrpAPrivate).ToHex(),
                Public = SrpInteger.FromByteArray(SrpAPublic).ToHex()
            };

            var serverEp = new SrpEphemeral
            {
                Secret = SrpInteger.FromByteArray(SrpBPrivate).ToHex(),
                Public = SrpInteger.FromByteArray(SrpBPublic).ToHex()
            };

            Assert.Equal(SrpV, SrpInteger.FromHex(verifier).ToByteArray());
            

            var clientSession = _srpClient.DeriveSession(clientEp.Secret, 
                serverEp.Public, _saltInt.ToHex(), SrpUser, privateKey);

            Assert.Equal(SrpK, SrpInteger.FromHex(clientSession.Key).ToByteArray());
            Assert.Equal(SrpM1, SrpInteger.FromHex(clientSession.Proof).ToByteArray());

            _srpClient.VerifySession(clientEp.Public, clientSession,
                SrpInteger.FromByteArray(SrpM2).ToHex());
        }

    }

}