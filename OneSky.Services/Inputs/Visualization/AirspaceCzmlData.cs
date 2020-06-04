using System;
using Newtonsoft.Json;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Airspace;

namespace OneSky.Services.Inputs.Visualization
{
    public class AirspaceCzmlData: IVerifiable
    {
        /// <summary>
        /// Options to select which airspaces are returned
        /// </summary>
        public AirspaceSelectionOptions AirspaceOptions { get; set; }
        /// <summary>
        /// The color the airspace will be rendered in. Default is Red.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// When true, the Airspace outline will be drawn. Default is true.
        /// </summary>
        public bool Outline { get; set; }
        /// <summary>
        /// The Airspace outline color. Default is black.
        /// </summary>
        public string OutlineColor { get; set; }
        /// <summary>
        /// How transparent the airspace will appear , 0 = invisible, 100 = opaque color. Default is 50.
        /// </summary>
        public int Translucency { get; set; }
        /// <summary>
        /// When the Airspace should begin displaying.  Default is DateTime.MinValue
        /// </summary>
        public DateTime DisplayStart { get; set; }
        /// <summary>
        /// When the Airspace will stop displaying.  Default is DateTime.MaxValue
        /// </summary>
        public DateTime DisplayStop { get; set; }

        public AirspaceCzmlData()
        {
            Color = System.Drawing.Color.Red.Name;
            Translucency = 50;
            DisplayStart = DateTime.MinValue;
            DisplayStop = DateTime.MaxValue;
            Outline = true;
            OutlineColor = System.Drawing.Color.Black.Name;
            AirspaceOptions = new AirspaceSelectionOptions();
        }

        public void Verify()
        {
            AirspaceOptions.Verify();

            if (Translucency < 0 || Translucency > 100)
            {
                throw new AnalyticalServicesException(23600, "Translucency must be within [0-100], exclusive.");
            }
            
            // check colors - set to defaults if unknown color is provided
            System.Drawing.Color c = System.Drawing.Color.FromName(Color);
            if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
            {
                Color = System.Drawing.Color.Red.Name;
            }
            c = System.Drawing.Color.FromName(OutlineColor);
            if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
            {
                OutlineColor = System.Drawing.Color.Black.Name;
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}