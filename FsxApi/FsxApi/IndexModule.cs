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

                var response = new
                {
                    Code = 200,
                    Message = "OK"
                };

                return Response.AsJson(response);
            };

            Get["/api/fsx"] = parameters =>
            {
                Console.WriteLine("GET /api/fsx");

                var fsxCommunicator = new Fsx(new ConsoleLogger());

                var planeData = fsxCommunicator.GetPlaneData();

                var response = new
                {
                    Code = 200,
                    Message = "OK",
                    Data = planeData
                };

                return Response.AsJson(response);
            };
        }
    }
}