using System.Collections.Generic;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Access
{
    public class OverflightAccessData<T>: IVerifiable
        where T: IVerifiable
    {
        public T Path { get; set; }
        public bool IncludePath { get; set; }
        public List<NamedRegion> Regions { get; set; }
        public List<string> CountryIDs { get; set; }

        #region IVerifiable Members

        public void Verify()
        {
            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}