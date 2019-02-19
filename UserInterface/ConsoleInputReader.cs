using System;
using System.Linq;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.UserInterface
{
    public class ConsoleInputReader : IInputReader
    {
        private const int STOP_CHOICE_AMOUNT_LIMIT = 10;

        // Should probably find a nice library that will do a better job than this quick and messy thing.
        public string ReadStopID(Dictionary<string, ILocation> availableStops) 
        {
            var stopName = ReadStop().ToLower();

            while(true)
            {
                var similarStops = new Dictionary<string, ILocation>();
                var containedStops = new Dictionary<string, ILocation>();

                foreach(var availableStop in availableStops) 
                {
                    var distance = Fastenshtein.Levenshtein.Distance(stopName, availableStop.Value.Alias.ToLower());

                    //Levenshtein distance is zero, thus the strings are equal
                    if(distance == 0)
                        return availableStop.Key;

                    if(availableStop.Value.Alias.ToLower().Contains(stopName) || stopName.Contains(availableStop.Value.Alias.ToLower()))
                        containedStops.Add(availableStop.Key, availableStop.Value);
                    else if(distance <= 5)
                        similarStops.Add(availableStop.Key, availableStop.Value);
                }

                var potentialStopIDs = new List<string>(containedStops.Keys.ToList());
                potentialStopIDs.AddRange(similarStops
                                        .OrderBy(x => Fastenshtein.Levenshtein.Distance(stopName, x.Value.Alias))
                                        .Select(x => x.Key)
                                        .ToList());

                System.Console.WriteLine("Couldn't find the stop you entered.");
                System.Console.WriteLine("Here are some similar sounding stops.");
                for(int i = 0; i < potentialStopIDs.Count && i < STOP_CHOICE_AMOUNT_LIMIT; i++)
                    System.Console.WriteLine(i + ". " + potentialStopIDs[i] + " " + availableStops[potentialStopIDs[i]].Alias);
                System.Console.Write("Enter a number from the list above, or try again with a new stop name: ");

                stopName = System.Console.ReadLine();
                int stopNumber;
                if(Int32.TryParse(stopName, out stopNumber))
                    return potentialStopIDs[stopNumber];
            }
        }

        public string ReadStop()
        {
            System.Console.Write("Input stop title: ");

            return System.Console.ReadLine();
        }

        public TimeSpan ReadTime() 
        {
            System.Console.Write("Input departure time(hh:mm): ");
            var departureTime = System.Console.ReadLine();
            var departureHours = Int32.Parse(departureTime.Split(':')[0]);
            var departureMinutes = Int32.Parse(departureTime.Split(':')[1]);

            return new TimeSpan(departureHours, departureMinutes, 0);
        }

        public bool ReadDoMoreSearches()
        {
            char input;
            bool doMoreSearches = false;
            bool isInputValid = false;

            while (!isInputValid)
            {
                System.Console.Write("Do more searches? [y/n]: ");
                input = System.Console.ReadLine().ToLower()[0];

                isInputValid = true;
                if (input == 'y')
                    doMoreSearches = true;
                else if (input == 'n')
                    doMoreSearches = false;
                else 
                {
                    System.Console.WriteLine("Invalid input.");
                    isInputValid = false;
                }
            } 

            return doMoreSearches;
        }
    }
}