using OneSky.Services.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneSky.Services.Outputs.Population
{
    public class PopulationResults
    {

        public double WeightedMean { get; set; }
        public double Mean { get; set; }
        public double SumOfWeights { get; set; }
        public List<double> PopulationValues { get; set; }
        public List<double> Weights { get; set; }

        public PopulationResults()
        {
            PopulationValues = new List<double>();
            Weights = new List<double>();
        }
        /// <summary>
        /// Sets or resets the Weights list to all ones.
        /// </summary>
        /// <param name="count">The number of 1's required in the Weights list.</param>
        public void SetWeightsToOne(int count)
        {
            Weights = new List<double>();
            for (var j = 0; j < count; j++)
            {
                Weights.Add(1.0);
            }
        }

        public void ComputeMeanAndWeightedMean()
        {
            if (PopulationValues.Count != Weights.Count && Weights.Count > 0)
            {
                throw new AnalyticalServicesException(10500,
                "The population values list must have the same number of items as the weights list, " +
                "and must contain at least one value.");
            }

            double tempWeight = 0;
            double tempMean = 0;
            for (var i = 0; i < PopulationValues.Count; i++)
            {
                tempWeight += PopulationValues[i] * Weights[i];
                tempMean += PopulationValues[i];
                SumOfWeights += Weights[i];
            }

            WeightedMean = tempWeight / SumOfWeights;
            Mean = tempMean / PopulationValues.Count;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
