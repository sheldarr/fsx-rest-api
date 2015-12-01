namespace FsxApi.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Location
    {
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }
    }
}
