using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExperimenting
{
    public class ProductionArea
    {
        public ProductionArea(string _republic, string _oblast = null, string _city = null)
        {
            Republic = _republic;
            Oblast = _oblast;
            City = _city;
        }

        [JsonProperty("Republic")]
        public string Republic { get; set; }

        [JsonProperty("Oblast")]
        public string Oblast { get; set; }

        //optional field, if null then entire oblast is used
        //if oblast is null or empty string then city will be set to null
        [JsonProperty("City")]
        public string City
        {
            get => _city;
            set => _city = string.IsNullOrEmpty(Oblast) ? null : value;
        }

        private string _city;

        public int ProductionAudit(string resource)
        {
            var availableQuantity = ExternalLogic.FromEachAccordingToHisAbilities(resource);
            var quantityLeftBehind = ExternalLogic.ToEachAccordingToHisNeeds(resource);
            switch (Republic)
            {
                case "Russia":
                    return resource == "Potato" ? availableQuantity - (int)quantityLeftBehind : 0;

                case "Ukraine":
                    return availableQuantity - (int)(quantityLeftBehind * 0.5);

                case "Khazakhstan":
                    return resource == "Potato" ? availableQuantity - (int)(quantityLeftBehind * new Random().Next(1, 10) / 10) : 0;

                default:
                    return resource == "Potato" ? new Random().Next(9999, 999999) : 0;
            }
        }
    }
}