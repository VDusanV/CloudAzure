using FilmService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LogKlijent
{
    class Program
    {
        static void Main(string[] args)
        {

            ILogcs proxy;
            var binding = new NetTcpBinding();
            ChannelFactory<ILogcs> factory = new ChannelFactory<ILogcs>(binding, new EndpointAddress("net.tcp://localhost:9000/InputRequestLog"));
            proxy = factory.CreateChannel();
            string s = "";
            while (true)
            {
                Console.WriteLine("Unesite 1 ukoliko zelite da vidite sve logove");
                s = Console.ReadLine();
                if (s == "1")
                    Console.WriteLine(proxy.SviLogovi());

            }
        }
    }
}
