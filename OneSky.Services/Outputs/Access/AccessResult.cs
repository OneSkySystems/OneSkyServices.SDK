using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Access
{
    /// <summary>
    /// Returns access times, and optionally the ephemeris for the to and from objects while within the access period.
    /// </summary>
    public class AccessResult<T>
        where T: IPathResult
    {
        public string AccessStart {get; set;}
        public string AccessStop { get; set; }
        public List<T> FromObjectPath { get; set; }
        public List<T> ToObjectPath { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}