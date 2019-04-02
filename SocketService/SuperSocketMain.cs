using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
{
    class SuperSocketMain
    {
        public static void Init()
        {
            try
            {
                Console.WriteLine("Welcome to SuperSocket SocketService!");
                IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
                if (!bootstrap.Initialize())
                {
                    Console.WriteLine("初始化失败");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("启动中...");
                var result = bootstrap.Start();
                foreach (var server in bootstrap.AppServers)
                {
                    if (server.State == ServerState.Running)
                    {
                        Console.WriteLine("- {0} 运行中", server.Name);
                    }
                    else
                    {
                        Console.WriteLine("- {0} 启动失败", server.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
