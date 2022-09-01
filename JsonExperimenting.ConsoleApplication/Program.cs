using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonExperimenting.ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var MyProductionAreas = new List<ProductionArea>
            {
                new ProductionArea("Ukraine", "Kyiv Oblast", "Kyiv"),
                new ProductionArea("Russia", "Moscow Oblast"),
                new ProductionArea("Khazakhstan")
            };
            var ResourceList = new List<string> { "Potato", "Hammer", "Sickle", "Wheat", "Truth", "Vodka", "Freedom" };
            var Collectivization = new CollectivizationEngine(MyProductionAreas, ResourceList);
            Collectivization.CollectivizeResources();
            Collectivization.SerializeMe();
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@"c:\Users\serge\Documents\Code\jsonExperiments\collectivization.json", JsonConvert.SerializeObject(Collectivization, Formatting.Indented));

            //// serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(@"c:\Users\serge\Documents\Code\jsonExperiments\collectivization.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();

            //    serializer.Serialize(file, Collectivization);
            //}
        }
    }
}