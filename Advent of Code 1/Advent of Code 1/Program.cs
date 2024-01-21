using System.Diagnostics;

namespace Advent_of_Code_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part 1 solution: " + Part1());
            Console.WriteLine("Part 2 solution: " + Part2());
        }
        static List<string> Read()
        {
            string fileName = "input.txt";
            List<string> lines = File.ReadAllLines(fileName).ToList();
            return lines;
        }

        static int Part1()
        {
            List<string> readText = Read();

            int sum = 0;

            foreach (string line in readText)
            {
                string lineNumber = "";

                for (int j = 0; j < line.Length; j++)
                {
                    if (lineNumber.Length == 0 && Char.IsDigit(line[j]))
                    {
                        lineNumber += line[j];
                        lineNumber += line[j];
                    }
                    else if (Char.IsDigit(line[j]))
                    {
                        char[] ch = lineNumber.ToCharArray();
                        ch[1] = line[j];
                        lineNumber = new string(ch);
                    }


                }
                if (lineNumber.Length > 0)
                {
                    sum += Convert.ToInt32(lineNumber);
                }
            }
            return sum;
        }

        static int Part2()
        {
            List<string> readText = Read();
            int sum = 0;
            Dictionary<string, char> numbers = new Dictionary<string, char>() { { "one", '1' }, { "two", '2' }, { "three", '3' }, { "four", '4' }, { "five", '5' }, { "six", '6' }, { "seven", '7' }, { "eight", '8' }, { "nine", '9' } };
            foreach (string line in readText)
            {
                string lineNumber = "";

                string linepiece = "", reverselinepiece = "";

                for (int i = 0; i < line.Length; i++)
                {
                    if (lineNumber.Length == 0 && Char.IsDigit(line[i]))
                    {
                        lineNumber += line[i];
                        lineNumber += line[i];
                    }
                    else if (Char.IsDigit(line[i]))
                    {
                        char[] ch = lineNumber.ToCharArray();
                        ch[1] = line[i];
                        lineNumber = new string(ch);
                    }
                    linepiece += line[i];

                    foreach (KeyValuePair<string, char> pair in numbers)
                    {
                        char[] pairK = pair.Key.ToCharArray();
                        Array.Reverse(pairK);
                        string pairRevKey = new string(pairK);

                        if (linepiece.Contains(pair.Key))
                        {
                            if (lineNumber.Length == 0)
                            {
                                lineNumber += pair.Value;
                                lineNumber += pair.Value;
                            }
                            else
                            {
                                char[] ch = lineNumber.ToCharArray();
                                ch[1] = pair.Value;
                                lineNumber = new string(ch);
                            }

                            linepiece = "";
                        }
                    }
                    
                }

                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (Char.IsDigit(line[i]))
                    {
                        char[] ch = lineNumber.ToCharArray();
                        ch[1] = line[i];
                        lineNumber = new string(ch);
                        break;
                    }
                    reverselinepiece += line[i];

                    foreach (KeyValuePair<string, char> pair in numbers)
                    {
                        char[] pairK = pair.Key.ToCharArray();
                        Array.Reverse(pairK);
                        string pairRevKey = new string(pairK);

                        if (reverselinepiece.Contains(pairRevKey))
                        {
                            char[] ch = lineNumber.ToCharArray();
                            ch[1] = pair.Value;
                            lineNumber = new string(ch);
                            reverselinepiece = "";
                            goto End;
                        }
                    }
                }
                End:
                    sum += Convert.ToInt32(lineNumber);
            }
                return sum;
        }


    }
}



