using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoSearch.utils
{
    class FileCheck
    {
        public static void CheckTrueFileName(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string bx = " ";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx += buffer.ToString();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            r.Close();
            fs.Close();
            //真实的文件类型
            Console.WriteLine(bx);
            //文件名，包括格式
            Console.WriteLine(System.IO.Path.GetFileName(path));
            //文件名， 不包括格式
            Console.WriteLine(System.IO.Path.GetFileNameWithoutExtension(path));
            //文件格式
            Console.WriteLine(System.IO.Path.GetExtension(path));
            Console.ReadLine();
        }
    }
}
