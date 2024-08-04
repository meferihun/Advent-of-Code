namespace Advent_of_Code_5;

class Program
{
    static void Main()
    {
        Console.WriteLine("Part 1 solution: " + Part1());
        Console.WriteLine("Part 2 solution: " + Part2());
    }

    static List<string> Read()
    {
        return File.ReadAllLines("input.txt").ToList();
    }

    static long Part1()
    {
        List<string> lines = Read();
        long lowestLocationNumber;

        List<long> seeds = Array
            .ConvertAll(lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), long.Parse).ToList();

        List<List<long>> seedToSoil = new List<List<long>>();
        List<List<long>> soilToFertilizer = new List<List<long>>();
        List<List<long>> fertilizerToWater = new List<List<long>>();
        List<List<long>> waterToLight = new List<List<long>>();
        List<List<long>> lightToTemperature = new List<List<long>>();
        List<List<long>> temperatureToHumidity = new List<List<long>>();
        List<List<long>> humidityToLocation = new List<List<long>>();

        Dictionary<string, List<List<long>>> mapDictionary = new Dictionary<string, List<List<long>>>()
        {
            { "seed-to-soil map:", seedToSoil },
            { "soil-to-fertilizer map:", soilToFertilizer },
            { "fertilizer-to-water map:", fertilizerToWater },
            { "water-to-light map:", waterToLight },
            { "light-to-temperature map:", lightToTemperature },
            { "temperature-to-humidity map:", temperatureToHumidity },
            { "humidity-to-location map:", humidityToLocation }
        };


        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];

            foreach (KeyValuePair<string, List<List<long>>> map in mapDictionary)
            {
                string mapName = map.Key;
                List<List<long>> mapNumbers = map.Value;

                if (line.StartsWith(mapName))
                {
                    int count = 1;

                    while (i + count < lines.Count && !string.IsNullOrWhiteSpace(lines[i + count]))
                    {
                        mapNumbers.Add(
                            Array.ConvertAll(lines[i + count].Split(' ', StringSplitOptions.RemoveEmptyEntries),
                                long.Parse).ToList());
                        count++;
                    }
                }
            }
        }

        List<long> locationNumbers = seeds.Select(seed => Part1ConvertSeedToLocation(seed, seedToSoil, soilToFertilizer, 
            fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation)).ToList();

        lowestLocationNumber = locationNumbers.Min();
        return lowestLocationNumber;
    }

    static long Part2()
    {
        List<string> lines = Read();
        long lowestLocationNumber;

        List<List<long>> seeds = new List<List<long>>();

        string[] numbers = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < numbers.Length; i += 2)
        {
            List<long> group = new List<long>();
            for (int j = 0; j < 2 && i + j < numbers.Length; j++)
            {
                long number = long.Parse(numbers[i + j]);
                group.Add(number);
            }
            seeds.Add(group);
        }

        List<List<long>> seedToSoil = new List<List<long>>();
        List<List<long>> soilToFertilizer = new List<List<long>>();
        List<List<long>> fertilizerToWater = new List<List<long>>();
        List<List<long>> waterToLight = new List<List<long>>();
        List<List<long>> lightToTemperature = new List<List<long>>();
        List<List<long>> temperatureToHumidity = new List<List<long>>();
        List<List<long>> humidityToLocation = new List<List<long>>();

        Dictionary<string, List<List<long>>> mapDictionary = new Dictionary<string, List<List<long>>>()
        {
            { "seed-to-soil map:", seedToSoil },
            { "soil-to-fertilizer map:", soilToFertilizer },
            { "fertilizer-to-water map:", fertilizerToWater },
            { "water-to-light map:", waterToLight },
            { "light-to-temperature map:", lightToTemperature },
            { "temperature-to-humidity map:", temperatureToHumidity },
            { "humidity-to-location map:", humidityToLocation }
        };


        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];

            foreach (KeyValuePair<string, List<List<long>>> map in mapDictionary)
            {
                string mapName = map.Key;
                List<List<long>> mapNumbers = map.Value;

                if (line.StartsWith(mapName))
                {
                    int count = 1;

                    while (i + count < lines.Count && !string.IsNullOrWhiteSpace(lines[i + count]))
                    {
                        mapNumbers.Add(
                            Array.ConvertAll(lines[i + count].Split(' ', StringSplitOptions.RemoveEmptyEntries),
                                long.Parse).ToList());
                        count++;
                    }
                }
            }
        }

        List<long> locationNumbers = seeds.Select(seed => Part2ConvertSeedToLocation(seed, seedToSoil, soilToFertilizer, 
            fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation)).ToList();

        lowestLocationNumber = locationNumbers.Min();
        return lowestLocationNumber;
    }

    static long Part1ConvertSeedToLocation(long seed, params List<List<long>>[] maps)
    {
        long currentNumber = seed;

        foreach (var map in maps)
        {
            currentNumber = ConvertNumber(currentNumber, map);
        }

        return currentNumber;
    }

    static long Part2ConvertSeedToLocation(List<long> seeds, params List<List<long>>[] maps)
    {
        long location = long.MaxValue;

        for (long seed = seeds[0]; seed < seeds[0] + seeds[1]; seed++)
        {
            long currentLocation = seed;
            
            int[] conversionOrder = Enumerable.Range(0,maps.Length).ToArray();

            foreach (int index in conversionOrder)
            {
                currentLocation = ConvertNumber(currentLocation, maps[index]);
            }

            if (currentLocation < location)
            {
                location = currentLocation;
            }
        }

        return location;
    }
    static long ConvertNumber(long number, List<List<long>> map)
    {
        foreach (var entry in map)
        {
            long destinationRangeStart = entry[0];
            long sourceRangeStart = entry[1];
            long rangeLength = entry[2];

            if (number >= sourceRangeStart && number < sourceRangeStart + rangeLength)
            {
                long offset = number - sourceRangeStart;
                long destinationNumber = destinationRangeStart + offset;

                if (destinationNumber != number) 
                {
                    number = destinationNumber;
                    break;
                }

            }
        }

        return number;
    }
}