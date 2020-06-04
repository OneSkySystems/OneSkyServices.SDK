using System;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;
using OneSky.Services.Inputs.Visualization;

namespace OneSky.Services.Inputs.Airspace
{
    public class AirspaceProximityCzmlData : AirspaceCzmlData, IVerifiable
    {
        /// <summary>
        /// The color the proximity boundary of the airspace will be rendered in. Default is Gold.
        /// </summary>
        public string ProximityColor { get; set; }

        /// <summary>
        /// How transparent the proximity boundary of the airspace will appear , 0 = invisible, 100 = opaque color. Default is 50.
        /// </summary>
        public int ProximityTranslucency { get; set; }

        public AirspaceProximityCzmlData()
        {
            ProximityColor = System.Drawing.Color.Gold.Name;
            ProximityTranslucency = 50;
        }

        public new void Verify()
        {

            base.Verify();

            if (ProximityTranslucency < 0 || ProximityTranslucency > 100)
            {
                throw new AnalyticalServicesException(23600, "ProximityTranslucency must be within [0-100], exclusive.");
            }

            // check colors - set to defaults if unknown color is provided
            System.Drawing.Color c = System.Drawing.Color.FromName(ProximityColor);
            if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
            {
                Color = System.Drawing.Color.Gold.Name;
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}