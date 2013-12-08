using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    class FileCheck
    {
        public static bool checkFileType(byte[] header)
        {
            //QuickTime,从第4字节开始判断6D , 6F,6F,76 ,对应ascii：....moov
            if (header[4] == 0x6d && header[5] == 0x6f && header[6] == 0x6f && header[7] == 0x76)
                return true;
            //RM,需要判断扩张情况,RM V10 和扩展对应的文件头MagicNumber的ASCII：.RMF
            //string strflags = System.Text.Encoding.ASCII.GetString(header,0,4/9);
            if (header[0] == 0x2e && header[1] == 0x52 && header[2] == 0x4d && header[3] == 0x46)
                return true;
            else if (header[0] == 0x2e && header[1] == 0x52 && header[2] == 0x4d && header[3] == 0x46 &&
                    header[4] == 0x00 && header[5] == 0x00 && header[6] == 0x00 && header[7] == 0x12 && header[8] == 0x00)
                return true;

            //SWF ,magic number="46.57.53"(FWS) , 压缩后43 57 53 （CWS）
            if (header[0] == 0x46 || header[0] == 0x43)
            {
                if (header[1] == 0x57 && header[2] == 0x53)
                    return true;
            }

            //FLV 
            if (header[0] == 0x46 && header[1] == 0x4C && header[2] == 0x56)
            {
                return true;
            }

            //WMV
            if (header[0] == 0x30 && header[1] == 0x26 && header[2] == 0xb2 && header[3] == 0x75 &&
              header[4] == 0x8e && header[5] == 0x66 && header[6] == 0xcf && header[7] == 0x11 &&
              header[8] == 0xa6 && header[9] == 0xd9 && header[10] == 0x00 && header[11] == 0xAA &&
              header[12] == 0x00 && header[13] == 0x62 && header[14] == 0xce && header[15] == 0x6c)
                return true;

            //AVI,判断前4个字节和后8个字节
            if (header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46 &&
                header[8] == 0x41 && header[9] == 0x56 && header[10] == 0x49 && header[11] == 0x20 &&
                header[12] == 0x4C && header[13] == 0x49 && header[14] == 0x53 && header[15] == 0x54)
                return true;

            return false;
        }
    }
}
