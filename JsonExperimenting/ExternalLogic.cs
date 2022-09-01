using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static JsonExperimenting.ErrorInformation;

namespace JsonExperimenting
{
    public class ExternalLogic
    {
        /// <summary>
        ///     Crunch the numbers on how productive the peasants are in specified location.
        /// </summary>
        /// <returns>quantity of specified <paramref name="resource"/> they've produced</returns>
        public static int FromEachAccordingToHisAbilities(string resource)
        {
            return resource switch
            {
                "Potato" => 99999999,
                "Hammer" => 2,
                "Sickle" => 2,
                "Wheat" => 100,
                "Truth" => 0,
                _ => new Random().Next(100000, 1000000),
            };
        }

        /// <summary>
        ///     What is the bare minimum to keep an acceptable margin of the proletariat alive?
        /// </summary>
        /// <returns>quantity of specified <paramref name="resource"/> we should ideally leave behind</returns>
        internal static int ToEachAccordingToHisNeeds(string resource)
        {
            return resource switch
            {
                "Potato" => 99999999,
                "Hammer" => 2,
                "Sickle" => 2,
                "Wheat" => 420,
                "Truth" => 0,
                _ => new Random().Next(10000, 240000),
            };
        }

        /// <summary>
        ///     Is the given resource contraband?
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        internal static void PolitburoInspection(string resource)
        {
            if (!new List<string> { "Freedom", "Happiness" }.Contains(resource))
                return;
            throw new PolitburoException("regime does not accept such notions");
        }

        public class SovieTimer
        {
            public TimeSpan Elapsed { get; internal set; }

            internal void Start()
            {
                Elapsed = new TimeSpan();
                return;
            }

            internal void Stop()
            {
                Elapsed = Elapsed.Add(new TimeSpan(420, 6, 6, 6));
                return;
            }
        }
    }
}