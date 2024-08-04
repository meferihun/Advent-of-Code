namespace Advent_of_Code_2
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

        static int Part1() { 
            List<string> lines = Read();
            int inputsAmount = 0;
            Dictionary<string, int> colors = new Dictionary<string, int>() { {"red", 12}, {"green", 13}, {"blue", 14} };

            foreach (string line in lines)
            {
                string[] game = line.Split(':');
                int id = int.Parse(game[0].Split(" ")[1]);
                string[] match = game[1].Split(";");

                bool valid = true;

                foreach(string current in match)
                {
                    string[] strings = current.Split(",");

                    foreach (string str in strings)
                    {
                        foreach (KeyValuePair<string, int> color in colors)
                        {
                            if (str.Contains(color.Key) && int.Parse(str.Split(' ')[1]) > color.Value)
                            {
                                valid = false;
                            }
                        }
                    }
                }
                if (valid)
                {
                    inputsAmount += id;
                }
            }
            return inputsAmount;
        }

        static int Part2()
        {
            List<string> lines = Read();
            int inputsAmount = 0;

            foreach (string line in lines)
            {
                Dictionary<string, int> colors = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 } };
                string[] game = line.Split(':');
                int id = int.Parse(game[0].Split(" ")[1]);
                string[] match = game[1].Split(";");

                foreach (string current in match)
                {
                    string[] strings = current.Split(",");

                    foreach (string str in strings)
                    {
                        int cubeAmount = int.Parse(str.Split(' ')[1]);
                        foreach (KeyValuePair<string, int> color in colors)
                        {
                            if (str.Contains(color.Key) && cubeAmount > color.Value)
                            {
                                colors[color.Key] = cubeAmount;
                            }
                        }
                    }
                }
                int output = 1;
                foreach (KeyValuePair<string, int> color in colors)
                {
                    output *= color.Value;
                }
                inputsAmount += output;
            }
            return inputsAmount;
        }
    }

    
}
