namespace FsxApi
{
    using System;
    using Interfaces;
    using Nancy.Hosting.Self;

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var baseUri = new Uri("http://localhost:7070");

            var hostConfiguration = new HostConfiguration
            {
                UrlReservations = new UrlReservations
                {
                    CreateAutomatically = true
                }
            };

            //var fsx = new Fsx(new ConsoleLogger());

            //var data = fsx.GetPlaneData();

            //Console.ReadLine();

            using (var host = new NancyHost(hostConfiguration, baseUri))
            {
                host.Start();

                Console.WriteLine("Application started at {0}:{1}", baseUri.Host, baseUri.Port);

                Console.ReadLine();
            }
        }
    }
}
