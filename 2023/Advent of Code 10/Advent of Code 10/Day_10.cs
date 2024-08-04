namespace Advent_of_Code_10;

class Program
{
    static void Main()
    {
        Console.WriteLine("Part 1 solution: " + Part1());
    }

    static List<string> Read()
    {
        return File.ReadLines("input.txt").ToList();
    }

    static long Part1()
    {
        List<string> lines = Read();

        List<long> startingPosition = new List<long>();

        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains('S'))
            {
                startingPosition.Add(lines[i].IndexOf('S'));
                startingPosition.Add(i);
            }
        }

        Dictionary<string, List<long>> directionDistances = new Dictionary<string, List<long>>()
        {
            {
                "up",
                new List<long>()
                    { startingPosition[0], startingPosition[1], 0, startingPosition[0], startingPosition[1] }
            },
            {
                "down",
                new List<long>()
                    { startingPosition[0], startingPosition[1], 0, startingPosition[0], startingPosition[1] }
            },
            {
                "left",
                new List<long>()
                    { startingPosition[0], startingPosition[1], 0, startingPosition[0], startingPosition[1] }
            },
            {
                "right",
                new List<long>()
                    { startingPosition[0], startingPosition[1], 0, startingPosition[0], startingPosition[1] }
            },
        };

        foreach (var direction in directionDistances)
        {
            for (long y = direction.Value[0]; y < lines.Count;)
            {
                for (long x = direction.Value[1]; x < lines[0].Length;)
                {
                    var floodfill = FloodFill(lines, direction.Value[3], direction.Value[4], x, y, lines[0].Length,
                        lines.Count, lines[(int)x][(int)y]);
                    if (floodfill.Contains("east"))
                    {
                        directionDistances["right"] = new List<long>()
                            { x + 1, y, directionDistances["right"][2]++, x, y };
                    }

                    if (floodfill.Contains("west"))
                    {
                        directionDistances["left"] = new List<long>()
                            { x - 1, y, directionDistances["left"][2]++, x, y };
                    }

                    if (floodfill.Contains("north"))
                    {
                        directionDistances["up"] = new List<long>() { x, y + 1, directionDistances["up"][2]++, x, y };
                    }

                    if (floodfill.Contains("south"))
                    {
                        directionDistances["down"] = new List<long>()
                            { x, y - 1, directionDistances["down"][2]++, x, y };
                    }
                }
            }
        }

        return directionDistances.Values.SelectMany(c => c).Max();
    }

    static string ReverseDirection(string direction)
    {
        Dictionary<string, string> directionPairs = new Dictionary<string, string>()
        {
            { "north", "south" },
            { "south", "north" },
            { "east", "west" },
            { "west", "east" }
        };

        return directionPairs.Where(x => x.Key == direction).Select(p => p.Value).First();
    }

    static List<string> SymbolDirection(char symbol)
    {
        Dictionary<char, List<string>> symbolDirections = new Dictionary<char, List<string>>()
        {
            { '|', new List<string>() { "north", "south" } },
            { '-', new List<string>() { "west", "east" } },
            { 'L', new List<string>() { "north", "east" } },
            { 'J', new List<string>() { "west", "north" } },
            { '7', new List<string>() { "south", "west" } },
            { 'F', new List<string>() { "south", "east" } },
            {'S', new List<string>() {"north, south, west, east"}}
        };

        return symbolDirections.Where(x => x.Key == symbol).Select(p => p.Value).First();
    }

    static bool IsValid(long x, long y, long previousX, long previousY, long lineMax, long columnMax, char nextSymbol,
        char currentSymbol)
    {
        if (x < 0 || x >= lineMax || y < 0 || y >= columnMax || nextSymbol == '.' || x == previousX || y == previousY ||
            nextSymbol == 'S' || !SymbolDirection(currentSymbol).Contains(ReverseDirection(SymbolDirection(nextSymbol)[0])))
            return false;
        return true;
    }

    static List<string> FloodFill(List<string> lines, long nextX, long nextY, long previousX, long previousY,
        long lineMax, long columnMax, char currentSymbol)
    {
        List<string> validDirections = new List<string>();

        if (IsValid(nextX + 1, nextY, previousX, previousY, lineMax, columnMax, lines[(int)nextX + 1][(int)nextY],
                currentSymbol))
        {
            validDirections.Add("east");
        }

        if (IsValid(nextX - 1, nextY, previousX, previousY, lineMax, columnMax, lines[(int)nextX - 1][(int)nextY],
                currentSymbol))
        {
            validDirections.Add("west");
        }

        if (IsValid(nextX, nextY + 1, previousX, previousY, lineMax, columnMax, lines[(int)nextX][(int)nextY + 1],
                currentSymbol))
        {
            validDirections.Add("north");
        }

        if (IsValid(nextX, nextY - 1, previousX, previousY, lineMax, columnMax, lines[(int)nextX][(int)nextY - 1],
                currentSymbol))
        {
            validDirections.Add("south");
        }

        return validDirections;
    }
}