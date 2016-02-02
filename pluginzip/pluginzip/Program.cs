using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pluginzip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("是否压缩当前目录的所有文件夹,y:是 n:否");
            string s = Console.ReadLine();
            if (s == "y")
            {
                string basepath = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo basedirinfo = new DirectoryInfo(basepath);
                foreach (DirectoryInfo dir in basedirinfo.GetDirectories())
                {
                    FastZipHelper.compress(dir.FullName, dir.FullName + ".zip");
                    Console.WriteLine("正在压缩" + dir.FullName + "...");
                }
                Console.WriteLine("压缩完成");
                Console.ReadLine();
            }
        }
    }
}
