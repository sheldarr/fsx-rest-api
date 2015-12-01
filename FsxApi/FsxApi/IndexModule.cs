namespace FsxApi
{
    using System;
    using Infrastructure;
    using Nancy;

    public class IndexModule : NancyModule
    {
        private readonly FsxCommunicator _fsxCommunicator;

        public IndexModule(FsxCommunicator fsxCommunicator)
        {
            _fsxCommunicator = fsxCommunicator;

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

                var planeData = _fsxCommunicator.GetPlaneData();

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