namespace FsxApi
{
    using System;
    using Nancy.Hosting.Self;

    class Program
    {
        static void Main(string[] args)
        {
            var baseUri = new Uri("http://localhost:7070");

            var hostConfiguration = new HostConfiguration
            {
                RewriteLocalhost = true,
                UrlReservations = new UrlReservations
                {
                    CreateAutomatically = true
                }
            };

            using (var host = new NancyHost(hostConfiguration, baseUri))
            {
                host.Start();

                Console.WriteLine("Application started at {0}:{1}", baseUri.Host, baseUri.Port);

                Console.ReadLine();
            }
        }
    }
}
