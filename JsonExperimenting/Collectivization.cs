using System;
using System.Collections.Generic;

using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static JsonExperimenting.ErrorInformation;

namespace JsonExperimenting
{
    /// <summary>
    ///     This class contains code for calculating logistics of collectivizing specified resources.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CollectivizationEngine
    {
        private List<string> ResourcesWeWant { get; set; } = new List<string> { "Potato", "Sickle", "Truth", "Happiness" };

        public CollectivizationEngine(List<ProductionArea> areas, List<string> resources)
        {
            ResourcesWeWant = resources;
            ProductionAreas = areas;
        }

        //how long it takes to collectivize all resources
        [JsonProperty("Duration")]
        public Duration Duration { get; set; } = new Duration();

        //list of places where our collectivization will proceed
        [JsonProperty("ProductionAreas")]
        public List<ProductionArea> ProductionAreas { get; set; }

        //list of resources we tried to collectivize
        [JsonProperty("CollectivizedResources")]
        public List<CollectivizedResource> CollectivizedResources { get; set; } = new List<CollectivizedResource>();

        public void CollectivizeResources()
        {
            var myTimer = new ExternalLogic.SovieTimer();
            myTimer.Start();
            foreach (var resource in ResourcesWeWant)
            {
                CollectivizedResources.Add(CollectivizeResource(resource, ProductionAreas));
            }
            myTimer.Stop();
            Duration.TimeSpentCollectivizing = myTimer.Elapsed;
        }

        public void SerializeMe()
        {
            File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Code\\jsonExperiments\\collectivization.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        private CollectivizedResource CollectivizeResource(string resource, List<ProductionArea> productionAreas)
        {
            var collectivizedResource = new CollectivizedResource
            {
                ResourceName = resource
            };

            try
            {
                ExternalLogic.PolitburoInspection(resource);
                var quantity = 0;
                foreach (var productionArea in productionAreas)
                {
                    quantity += productionArea.ProductionAudit(resource);
                }
                if (quantity < 0) throw new AuditError("the bourgoisie have sabotaged our five-year plan");

                collectivizedResource.Count = quantity;
                if (quantity == 0)
                {
                    collectivizedResource.CollectivizationStatus = "Skipped";
                    collectivizedResource.LogMessage = $"Resource '{resource}' is neither produced nor consumed by population of specified areas.";
                }
                else
                {
                    collectivizedResource.CollectivizationStatus = "Succeeded";
                    collectivizedResource.LogMessage = $"Successfully collectivized {quantity} units of resource '{resource}'.";
                }
            }
            catch (PolitburoException ex)
            {
                collectivizedResource.ErrorInformation = new ErrorInformation
                {
                    ErrorCode = 666,
                    ErrorMessage = ex.Message,
                    ErrorType = "PolitburoException"
                };
            }
            catch (AuditError ex)
            {
                collectivizedResource.ErrorInformation = new ErrorInformation
                {
                    ErrorCode = 404,
                    ErrorMessage = ex.Message,
                    ErrorType = "ForeignInfluence"
                };
            }
            catch (Exception)
            {
                collectivizedResource.ErrorInformation = new ErrorInformation
                {
                    ErrorMessage = "Unknown error has occurred.",
                    ErrorType = "Unknown"
                };
            }

            if (collectivizedResource.ErrorInformation == null)
            {
                return collectivizedResource;
            }
            collectivizedResource.CollectivizationStatus = "Failed";
            collectivizedResource.LogMessage = $"Collectivization of resource '{resource}' has failed with Error Code [{collectivizedResource.ErrorInformation.ErrorCode}] : {collectivizedResource.ErrorInformation.ErrorMessage}";
            return collectivizedResource;
        }
    }

    public class Duration
    {
        public TimeSpan TimeSpentCollectivizing { get; set; } = new TimeSpan();

        [JsonProperty("days")]
        public int Days => TimeSpentCollectivizing.Days;

        [JsonProperty("hours")]
        public float Hours => (float)(TimeSpentCollectivizing.Hours + Math.Round((decimal)(TimeSpentCollectivizing.Minutes / 60), 2));
    }
}