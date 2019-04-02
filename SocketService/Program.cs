using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("正在进入...");
                SuperSocketMain.Init();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
