namespace FsxApi
{
    using System;
    using Infrastructure;
    using Nancy;

    public class IndexModule : NancyModule
    {
        private readonly FsxConnection _fsxConnection;

        public IndexModule(FsxConnection fsxConnection)
        {
            _fsxConnection = fsxConnection;

            Get["/"] = parameters =>
            {
                Console.WriteLine("GET /");

                var response = new
                {
                    Message = "OK"
                };

                return Response.AsJson(response);
            };

            Get["/api/fsx"] = parameters =>
            {
                Console.WriteLine("GET /api/fsx");

                var planeData = _fsxConnection.GetPlanePosition();

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