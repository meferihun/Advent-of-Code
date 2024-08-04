using Microsoft.VisualBasic;

namespace Advent_of_Code_9;

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

        long extrapolatedValue = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            next:
            string line = lines[i];
            List<List<long>> actualLineDifferences = new List<List<long>>()
                { line.Split(' ').Select(s => long.Parse(s)).ToList() };
            for (int j = 0; j < actualLineDifferences.Count; j++)
            {
                List<long> actualDifference = new List<long>();
                for (int k = 0; k < actualLineDifferences[j].Count; k++)
                {
                    if (k + 1 < actualLineDifferences[j].Count)
                    {
                        actualDifference.Add(actualLineDifferences[j][k + 1] - actualLineDifferences[j][k]);
                    }
                }

                actualLineDifferences.Add(actualDifference);
                if (actualDifference.Count(e => e == 0) == actualDifference.Count)
                {
                    for (int l = actualLineDifferences.Count - 1; l >= 0; l--)
                    {
                        if (l + 1 < actualLineDifferences.Count)
                        {
                            actualLineDifferences[l].Add(actualLineDifferences[l].LastOrDefault() + actualLineDifferences[l+1].LastOrDefault());
                        }
                        else
                        {
                            actualLineDifferences[l].Add(actualLineDifferences[l].LastOrDefault());
                        }
                    }
                    extrapolatedValue += actualLineDifferences[0].LastOrDefault();
                    if (i + 1 < lines.Count)
                    {
                        i++;
                        goto next;
                    }

                    goto end;
                }
            }
        }

        end:
        return extrapolatedValue;
    }
    
    static long Part2()
    {
        List<string> lines = Read();

        long extrapolatedValue = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            next:
            string line = lines[i];
            List<List<long>> actualLineDifferences = new List<List<long>>()
                { line.Split(' ').Select(s => long.Parse(s)).ToList() };
            for (int j = 0; j < actualLineDifferences.Count; j++)
            {
                List<long> actualDifference = new List<long>();
                for (int k = 0; k < actualLineDifferences[j].Count; k++)
                {
                    if (k + 1 < actualLineDifferences[j].Count)
                    {
                        actualDifference.Add(actualLineDifferences[j][k + 1] - actualLineDifferences[j][k]);
                    }
                }

                actualLineDifferences.Add(actualDifference);
                if (actualDifference.Count(e => e == 0) == actualDifference.Count)
                {
                    for (int l = actualLineDifferences.Count - 1; l >= 0; l--)
                    {
                        if (l + 1 < actualLineDifferences.Count)
                        {
                            actualLineDifferences[l].Insert(0, actualLineDifferences[l].FirstOrDefault() - actualLineDifferences[l+1].FirstOrDefault());
                        }
                        else
                        {
                            actualLineDifferences[l].Insert(0, 0 - actualLineDifferences[l].FirstOrDefault());
                        }
                    }
                    extrapolatedValue += actualLineDifferences[0].FirstOrDefault();
                    if (i + 1 < lines.Count)
                    {
                        i++;
                        goto next;
                    }

                    goto end;
                }
            }
        }

        end:
        return extrapolatedValue;
    }
}