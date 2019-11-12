using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Communications;
using OneSky.Services.Outputs.Communications;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Communications
{
    /// <summary>
    /// Communication methods.  See the service documentation for 
    /// notes this service: https://saas.agi.com/V1/Documentation/Communication.
    /// </summary>
    public class CommunicationServices
    {
        public static async Task<CommunicationsResults> GetLinkBudget(CommunicationData commData){
            string relativeUri = ServiceUris.CommunicationsLinkBudgetUri;
            
            commData.Verify();                        
                      
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<CommunicationData, CommunicationsResults>(uri, commData);
        }        
    }
}