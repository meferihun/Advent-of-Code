namespace Advent_of_Code_6;

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

    static int Part1()
    {
        List<string> lines = Read();

        int amountsToBeat = 0;
        List<int> times = Array
            .ConvertAll(lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse).ToList();
        List<int> distances = Array
            .ConvertAll(lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse).ToList();

        for (int i = 0; i < times.Count; i++)
        {
            int time = times[i];
            int distance = distances[i];
            int beats = 0;

            for (int j = 0; j < time; j++)
            {
                if (j * (time - j) > distance)
                {
                    beats++;
                }
            }

            if (amountsToBeat == 0)
            {
                amountsToBeat = beats;
            }
            else
            {
                amountsToBeat *= beats;
            }
        }

        return amountsToBeat;
    }

    static long Part2()
    {
        List<string> lines = Read();

        long amountsToBeat = 0;
        long time = long.Parse(lines[0].Split(':',StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));
        long distance = long.Parse(lines[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));
        long beats = 0;

        for (long j = 0; j < time; j++)
        {
            if (j * (time - j) > distance)
            {
                beats++;
            }
        }

        if (amountsToBeat == 0)
        {
            amountsToBeat = beats;
        }
        else
        {
            amountsToBeat *= beats;
        }

        return amountsToBeat;
    }
}