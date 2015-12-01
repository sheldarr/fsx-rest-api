namespace FsxApi.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PlaneData
    {
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }
}
