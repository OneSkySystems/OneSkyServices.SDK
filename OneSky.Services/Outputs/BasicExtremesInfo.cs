using Newtonsoft.Json;

namespace OneSky.Services.Outputs
{
    public class BasicExtremesInfo<T>
    {
        public T Max { get; set; }
        public T Min { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
