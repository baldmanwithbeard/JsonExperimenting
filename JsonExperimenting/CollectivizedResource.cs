using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExperimenting
{
    public class CollectivizedResource
    {
        [JsonProperty("ResourceName")]
        public string ResourceName { get; set; }

        [JsonProperty("CollectivizationStatus")]
        public string CollectivizationStatus { get; set; }

        [JsonProperty("Count")]
        public long? Count { get; set; }

        [JsonProperty("ErrorInformation")]
        public ErrorInformation ErrorInformation { get; set; }

        [JsonProperty("LogMessage")]
        public string LogMessage { get; set; }
    }
}