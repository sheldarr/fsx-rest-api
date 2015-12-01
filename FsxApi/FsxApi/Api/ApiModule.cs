namespace FsxApi.Api
{
    using System;
    using Fsx;
    using Nancy;

    public class ApiModule : NancyModule
    {
        private readonly FsxConnection _fsxConnection;

        public ApiModule(FsxConnection fsxConnection)
        {
            _fsxConnection = fsxConnection;

            Get["/api/planePosition"] = parameters =>
            {
                Console.WriteLine("GET /api/planePosition");

                var planePosition = _fsxConnection.GetPlanePosition();

                return Response.AsJson(planePosition);
            };
        }
    }
}