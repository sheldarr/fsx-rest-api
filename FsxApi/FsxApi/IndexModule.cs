namespace FsxApi
{
    using System;
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                Console.WriteLine("GET /");

                return Response.AsJson("Application running");
            };
        }
    }
}