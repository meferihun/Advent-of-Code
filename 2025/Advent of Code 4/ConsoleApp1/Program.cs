using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        private byte[] grid;
        private int fieldWidth;
        private int fieldHeight;

        public Program()
        {
            var rawLines = File.ReadAllLines("input.txt");
            fieldHeight = rawLines.Length;
            fieldWidth = rawLines[0].Length;

            grid = new byte[fieldWidth * fieldHeight];

            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    grid[y * fieldWidth + x] = (byte)(rawLines[y][x] == '@' ? 1 : 0);
                }
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine($"Part 1: {program.Part1()}");
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Restart();

            Console.WriteLine($"Part 2: {program.Part2()}");
            stopwatch.Stop();
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private int Part1()
        {
            var result = 0;

            for (int y = 0; y < fieldHeight; y++)
            {
                int rowOffset = y * fieldWidth;
                for (int x = 0; x < fieldWidth; x++)
                {
                    if (grid[rowOffset + x] == 0)
                    {
                        continue;
                    }

                    int count = CheckArea(y, x);
                    if (count < 4)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private int Part2()
        {
            var result = 0;
            var removed = true;

            int[] toRemove = new int[grid.Length];
            int toRemoveCount;

            while (removed)
            {
                removed = false;
                toRemoveCount = 0;

                for (var y = 0; y < fieldHeight; y++)
                {
                    int rowOffset = y * fieldWidth;

                    for (var x = 0; x < fieldWidth; x++)
                    {
                        int currentIdx = rowOffset + x;
                        if (grid[currentIdx] == 0)
                        {
                            continue;
                        }

                        int count = 0;

                        if (y > 0)
                        {
                            int topOffset = rowOffset - fieldWidth;
                            count += grid[topOffset + x];

                            if (x > 0)
                            {
                                count += grid[topOffset + x - 1];
                            }

                            if (x < fieldWidth - 1)
                            {
                                count += grid[topOffset + x + 1];
                            }
                        }

                        if (y < fieldHeight - 1)
                        {
                            int bottomOffset = rowOffset + fieldWidth;
                            count += grid[bottomOffset + x];
                            if (x > 0)
                            {
                                count += grid[bottomOffset + x - 1];
                            }

                            if (x < fieldWidth - 1)
                            {
                                count += grid[bottomOffset + x + 1];
                            }
                        }

                        if (x > 0)
                        {
                            count += grid[rowOffset + x - 1];
                        }

                        if (x < fieldWidth - 1)
                        {
                            count += grid[rowOffset + x + 1];
                        }

                        if (count < 4)
                        {
                            toRemove[toRemoveCount++] = currentIdx;
                            removed = true;
                            result++;
                        }
                    }
                }

                if (removed)
                {
                    for (int i = 0; i < toRemoveCount; i++)
                    {
                        grid[toRemove[i]] = 0;
                    }
                }
            }

            return result;
        }

        private int CheckArea(int y, int x)
        {
            int count = 0;
            int rowOffset = y * fieldWidth;

            if (y > 0)
            {
                int topOffset = rowOffset - fieldWidth;
                count += grid[topOffset + x];

                if (x > 0)
                {
                    count += grid[topOffset + x - 1];
                }

                if (x < fieldWidth - 1)
                {
                    count += grid[topOffset + x + 1];
                }
            }

            if (y < fieldHeight - 1)
            {
                int bottomOffset = rowOffset + fieldWidth;
                count += grid[bottomOffset + x];

                if (x > 0)
                {
                    count += grid[bottomOffset + x - 1];
                }

                if (x < fieldWidth - 1)
                {
                    count += grid[bottomOffset + x + 1];
                }
            }

            if (x > 0)
            {
                count += grid[rowOffset + x - 1];
            }

            if (x < fieldWidth - 1)
            {
                count += grid[rowOffset + x + 1];
            }

            return count;
        }
    }
}
