using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Access
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of position data returned.null  
    /// Usually ServiceCartographicWithTime or ServiceCartesianWithTime</typeparam>
    public class OverflightAccessResult<T>
        where T: IPathResult
    {
        public string CountryId {get; set;}
        public string Name { get; set; }
        public T Entry { get; set; }
        public T Exit { get; set; }
        public List<T> Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}