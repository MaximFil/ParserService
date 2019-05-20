using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ParserService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        { using (ParserContext context = new ParserContext())
            {
                User user1 = new User { Name = "Maxim", Age = 18 };
                context.Users.Add(user1);
                context.SaveChanges();
            }
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
           
        }
    }
}
