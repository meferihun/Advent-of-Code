namespace Advent_of_Code_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Read();
            var parsedInput = ParseInput(input);

            Console.WriteLine($"Part 1: {Part1(parsedInput)}");
            Console.WriteLine($"Part 2: {Part2(parsedInput)}");
        }

        private static List<string> Read()
        {
            var fileName = "input.txt";
            return File.ReadAllLines(fileName).ToList();
        }

        private static List<List<long>> ParseInput(List<string> lines)
        {
            var result = new List<List<long>>();
            foreach (var line in lines)
            {
                var row = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('-').Select(long.Parse).ToList()).ToList();
                result.AddRange(row);
            }
            return result;
        }

        private static long Part1(List<List<long>> parsedInput)
        {
            var result = 0L;

            foreach (var row in parsedInput)
            {
                var minValue = row.First();
                var maxValue = row.Last();

                var minLength = minValue.ToString().Length;
                var maxLength = maxValue.ToString().Length;

                for (var length = minLength; length <= maxLength; length++)
                {
                    if (length % 2 != 0)
                    {
                        continue;
                    }

                    var rootLength = length / 2;

                    var multiplier = (long)Math.Pow(10, rootLength) + 1;

                    var absoluteMinValue = (long)Math.Pow(10, rootLength - 1);
                    var rootMin = Math.Max(absoluteMinValue, minValue / multiplier);
                    var rootMax = maxValue / multiplier;

                    for (var root = rootMin; root <= rootMax; root++)
                    {
                        var candidate = root * multiplier;
                        if (candidate >= minValue && candidate <= maxValue && candidate.ToString().Length == length)
                        {
                            result += candidate;
                        }
                    }
                }
            }

            return result;
        }

        private static long Part2(List<List<long>> parsedInput)
        {
            var totalSum = 0L;

            foreach (var row in parsedInput)
            {
                var minValue = row.First();
                var maxValue = row.Last();

                var minLength = minValue.ToString().Length;
                var maxLength = maxValue.ToString().Length;

                var rowCandidates = new HashSet<long>();

                for (var length = minLength; length <= maxLength; length++)
                {
                    for (var blockLength = 1; blockLength <= length / 2; blockLength++)
                    {
                        if (length % blockLength != 0)
                        {
                            continue;
                        }

                        var repeatCount = length / blockLength;

                        var multiplier = 0L;
                        for (int j = 0; j < repeatCount; j++)
                        {
                            multiplier += (long)Math.Pow(10, blockLength * j);
                        }

                        var absoluteMinValue = (long)Math.Pow(10, blockLength - 1);

                        var rootMin = Math.Max(absoluteMinValue, minValue / multiplier);
                        var rootMax = maxValue / multiplier;

                        for (var root = rootMin; root <= rootMax; root++)
                        {
                            var candidate = root * multiplier;

                            if (candidate >= minValue && candidate <= maxValue && candidate.ToString().Length == length)
                            {
                                rowCandidates.Add(candidate);
                            }
                        }
                    }
                }

                totalSum += rowCandidates.Sum();
            }

            return totalSum;
        }

    }
}
