namespace Advent_of_Code_8;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Part 1 solution: " + Part1());
        Console.WriteLine("Part 2 solution: " + Part2());
    }

    static List<string> Read()
    {
        return File.ReadLines("input.txt").ToList();
    }

    static long Part1()
    {
        List<string> lines = Read();

        long steps = 0;

        string direction = "";
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            if (!line.Contains('=') && !string.IsNullOrWhiteSpace(line))
            {
                foreach (var c in line.Trim())
                {
                    if (c == 'L')
                    {
                        direction += '0';
                    }
                    else
                    {
                        direction += '1';
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                map[line.Split('=')[0].Trim()] = line.Split('=')[1].Trim().Replace("(", "").Replace(")", "").Split(',')
                    .Select(s => s.Trim()).ToList();
            }
        }

        string actualStep = "AAA";
        for (int i = 0; i <= direction.Length; i++)
        {
            if (i == direction.Length)
            {
                i = 0;
            }

            char c = direction[i];

            if (actualStep == "ZZZ")
            {
                break;
            }

            foreach (var step in map)
            {
                if (actualStep == step.Key)
                {
                    actualStep = step.Value[int.Parse(c.ToString())];
                    steps++;
                    break;
                }
            }
        }

        return steps;
    }

    static long Part2()
    {
        List<string> lines = Read();

        string direction = "";
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        Dictionary<string, List<long>> actualSteps = new Dictionary<string, List<long>>();

        foreach (var line in lines)
        {
            if (!line.Contains('=') && !string.IsNullOrWhiteSpace(line))
            {
                foreach (var c in line.Trim())
                {
                    if (c == 'L')
                    {
                        direction += '0';
                    }
                    else
                    {
                        direction += '1';
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                map[line.Split('=')[0].Trim()] = line.Split('=')[1].Trim().Replace("(", "").Replace(")", "").Split(',')
                    .Select(s => s.Trim()).ToList();
                if (line.Split('=')[0].Trim()[2] == 'A')
                {
                    actualSteps[line.Split('=')[0].Trim()] = new List<long>();
                }
            }
        }

        for (int j = 0; j < actualSteps.Count; j++)
        {
            next:
            long steps = 0;
            string actualStep = actualSteps.Keys.ElementAt(j);
            string current = actualStep;
            for (int i = 0; i <= direction.Length; i++)
            {
                if (i == direction.Length)
                {
                    i = 0;
                }

                char side = direction[i];

                foreach (var step in map)
                {
                    if (current == step.Key)
                    {
                        current = step.Value[int.Parse(side.ToString())];
                        steps++;
                        if (current.EndsWith('Z'))
                        {
                            actualSteps[actualStep].Add(steps);
                            if (j + 1 < actualSteps.Count)
                            {
                                j++;
                                goto next;
                            }
                            else
                            {
                                goto end;
                            }
                        }

                        break;
                    }
                }
            }
        }

        end:
        return actualSteps.Values.SelectMany(list => list).Aggregate((a, b) => LCM(a, b));
    }

    static long GCD(long a, long b)
    {
        if (b == 0)
            return a;
        return GCD(b, a % b);
    }

    static long LCM(long a, long b)
    {
        return (a / GCD(a, b)) * b;
    }
}