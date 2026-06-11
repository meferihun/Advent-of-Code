using System.Diagnostics;

namespace Advent_of_Code_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Read();

            var watch = Stopwatch.StartNew();
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 1 Time: {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            Console.WriteLine($"Part 2: {Part2(input)}");
            Console.WriteLine($"Part 2 Time: {watch.ElapsedMilliseconds} ms");
            watch.Stop();
        }

        private static List<string> Read()
        {
            var fileName = "input.txt";
            return File.ReadAllLines(fileName).ToList();
        }

        private static int Part1(List<string> input)
        {
            var result = 0;

            foreach (var item in input)
            {
                var maxTwoDigit = -1;
                var maxRightChar = '/';

                for (var i = item.Length - 1; i >= 0; i--)
                {
                    var currentChar = item[i];

                    if (maxRightChar != '/')
                    {
                        var currentNum = (currentChar - '0') * 10 + (maxRightChar - '0');

                        if (currentNum > maxTwoDigit)
                        {
                            maxTwoDigit = currentNum;
                        }
                    }

                    if (currentChar > maxRightChar)
                    {
                        maxRightChar = currentChar;
                    }
                }

                if (maxTwoDigit != -1)
                {
                    result += maxTwoDigit;
                }
            }

            return result;
        }

        private static long Part2(List<string> input)
        {
            var result = 0L;

            var stack = new char[12];

            foreach (var item in input)
            {
                var length = item.Length;

                var stackPointer = 0;

                for (int i = 0; i < length; i++)
                {
                    var currentChar = item[i];

                    var remainingCharCount = length - i;

                    while (stackPointer > 0 && currentChar > stack[stackPointer - 1] && (stackPointer - 1 + remainingCharCount) >= 12)
                    {
                        stackPointer--;
                    }

                    if (stackPointer < 12)
                    {
                        stack[stackPointer] = currentChar;
                        stackPointer++;
                    }
                }

                if (stackPointer == 12)
                {
                    var num = 0L;
                    for (int i = 0; i < 12; i++)
                    {
                        num = num * 10 + (stack[i] - '0');
                    }
                    result += num;
                }
            }

            return result;
        }

    }
}
