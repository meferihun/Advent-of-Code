namespace Advent_of_Code_3
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Part 1 solution: " + Part1());
            //Console.WriteLine("Part 2 solution: " + Part2());
        }

        static List<string> Read()
        {
            string fileName = "input.txt";
            List<string> lines = File.ReadAllLines(fileName).ToList();
            return lines;
        }

        static int Part1()
        {
            List<string> lines = Read();
            int partNumSum = 0;
            char[] symbols = { '#', '&', '+', '-', '*', '$', '/', '=', '%', '@' };

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                int lineNumSum = 0;
                bool 
                    isNumber = false,
                    isSymbol = false;
                
                for (int j = 0; j < line.Length; j++)
                {
                    int startIndex = j;
                    int endIndex = j;
                    if (Char.IsDigit(line[j]))
                    {
                        
                        string number = "";

                        while (endIndex < line.Length && Char.IsDigit(line[endIndex]))
                        {
                            number += line[endIndex];
                            endIndex++;
                        }

                        j = endIndex - 1;

                        if (startIndex > 0 && !Char.IsDigit(line[startIndex - 1]) && line[startIndex - 1] != '.')
                        {
                            lineNumSum += int.Parse(number);
                            isNumber = true;
                        }
                        else if (endIndex < line.Length && !Char.IsDigit(line[endIndex]) && line[endIndex] != '.')
                        {
                            lineNumSum += int.Parse(number);
                            isNumber = true;
                        }
                        else if (startIndex > 0 && endIndex < line.Length)
                        {
                            if (i > 0 && i < lines.Count - 1)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex + 1)];
                                string after = lines[i + 1][(startIndex - 1)..(endIndex + 1)];

                                if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                {
                                    lineNumSum += int.Parse(number);
                                    isNumber = true;
                                }
                            }
                            else if (i == 0)
                            {
                                string after = lines[i + 1][(startIndex - 1)..(endIndex + 1)];
                                if (after.IndexOfAny(symbols) != -1)
                                {
                                    lineNumSum += int.Parse(number);
                                    isNumber = true;
                                }
                            }
                            else if (endIndex < line.Length)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex + 1)];
                                if (before.IndexOfAny(symbols) != -1)
                                {
                                    lineNumSum += int.Parse(number);
                                    isNumber = true;
                                }
                            }
                        }
                        else if (endIndex == line.Length)
                        {
                            if (i > 0 && i < lines.Count - 1)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex)];
                                string after = lines[i + 1][(startIndex - 1)..(endIndex)];

                                if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                {
                                    lineNumSum += int.Parse(number);
                                    isNumber = true;
                                }
                            }
                            else if (startIndex == 0)
                            {
                                if (i > 0 && i < lines.Count)
                                {
                                    string before = lines[i - 1][(startIndex)..(endIndex + 1)];
                                    string after = lines[i + 1][(startIndex)..(endIndex + 1)];

                                    if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                    {
                                        lineNumSum += int.Parse(number);
                                        isNumber = true;
                                    }
                                }
                                else if (i == 0)
                                {
                                    string after = lines[i + 1][(startIndex)..(endIndex + 1)];
                                    if (after.IndexOfAny(symbols) != -1)
                                    {
                                        lineNumSum += int.Parse(number);
                                        isNumber = true;
                                    }
                                }
                                else if (endIndex < line.Length)
                                {
                                    string before = lines[i - 1][(startIndex)..(endIndex + 1)];
                                    if (before.IndexOfAny(symbols) != -1)
                                    {
                                        lineNumSum += int.Parse(number);
                                        isNumber = true;
                                    }
                                }
                            }
                        }
                    } else if (symbols.Contains(line[j]))
                    {
                        isSymbol = true;
                    }

                    if (isNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        for (int k = startIndex; k < endIndex; k++)
                        {
                            Console.Write(line[k]);
                        }
                        Console.ResetColor();
                        isNumber = false;
                    } 
                    else if (isSymbol)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(line[j]);
                        Console.ResetColor();
                        isSymbol = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (startIndex == endIndex)
                        {
                                Console.Write(line[j]);
                        }
                        else
                        {
                            for (int k = startIndex; k < endIndex; k++)
                            {
                                Console.Write(line[k]);
                            }
                        }
                        
                        Console.ResetColor();
                    }
                }
                partNumSum += lineNumSum;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(" " + lineNumSum);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" " + partNumSum); Console.ResetColor();
                Console.WriteLine();
            }
            return partNumSum;
        }
        /*
        static int Part2()
        {
            List<string> lines = Read();
            int partNumSum = 0;
            char symbol = '*';

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                
                for (int j = 0; j < line.Length; j++)
                {
                    if (Char.IsDigit(line[j]))
                    {
                        int startIndex = j;
                        int endIndex = j;
                        string number1 = "";
                        string number2 = "";

                        while (endIndex < line.Length && Char.IsDigit(line[endIndex]))
                        {
                            number1 += line[endIndex];
                            endIndex++;
                        }

                        j = endIndex - 1;

                        if (startIndex > 0 && !Char.IsDigit(line[startIndex - 1]) && line[startIndex - 1] != '.')
                        {
                            partNumSum += int.Parse(number);
                        }
                        else if (endIndex < line.Length && !Char.IsDigit(line[endIndex]) && line[endIndex] != '.')
                        {
                            partNumSum += int.Parse(number);
                        }
                        else if (startIndex > 0 && endIndex < line.Length)
                        {
                            if (i > 0 && i < lines.Count - 1)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex + 1)];
                                string after = lines[i + 1][(startIndex - 1)..(endIndex + 1)];

                                if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                {
                                    partNumSum += int.Parse(number);
                                }
                            }
                            else if (i == 0)
                            {
                                string after = lines[i + 1][(startIndex - 1)..(endIndex + 1)];
                                if (after.IndexOfAny(symbols) != -1)
                                {
                                    partNumSum += int.Parse(number);
                                }
                            }
                            else if (endIndex < line.Length)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex + 1)];
                                if (before.IndexOfAny(symbols) != -1)
                                {
                                    partNumSum += int.Parse(number);
                                }
                            }
                        }
                        else if (endIndex == line.Length)
                        {
                            if (i > 0 && i < lines.Count - 1)
                            {
                                string before = lines[i - 1][(startIndex - 1)..(endIndex)];
                                string after = lines[i + 1][(startIndex - 1)..(endIndex)];

                                if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                {
                                    partNumSum += int.Parse(number);
                                }
                            }
                            else if (startIndex == 0)
                            {
                                if (i > 0 && i < lines.Count)
                                {
                                    string before = lines[i - 1][(startIndex)..(endIndex + 1)];
                                    string after = lines[i + 1][(startIndex)..(endIndex + 1)];

                                    if (before.IndexOfAny(symbols) != -1 || after.IndexOfAny(symbols) != -1)
                                    {
                                        partNumSum += int.Parse(number);
                                    }
                                }
                                else if (i == 0)
                                {
                                    string after = lines[i + 1][(startIndex)..(endIndex + 1)];
                                    if (after.IndexOfAny(symbols) != -1)
                                    {
                                        partNumSum += int.Parse(number);
                                    }
                                }
                                else if (endIndex < line.Length)
                                {
                                    string before = lines[i - 1][(startIndex)..(endIndex + 1)];
                                    if (before.IndexOfAny(symbols) != -1)
                                    {
                                        partNumSum += int.Parse(number);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return partNumSum;
        }
        */
    }
}