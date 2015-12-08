namespace FsxApi.Api
{
    using System;
    using Fsx;
    using Nancy;

    public class ApiModule : NancyModule
    {
        private readonly FsxDataRepository _fsxDataRepository;

        public ApiModule(FsxDataRepository fsxDataRepository)
        {
            _fsxDataRepository = fsxDataRepository;

            Get["/api/planePosition"] = parameters =>
            {
                Console.WriteLine("GET /api/planePosition");

                var planePosition = _fsxDataRepository.GetPlanePosition();

                return Response.AsJson(planePosition);
            };
        }
    }
}