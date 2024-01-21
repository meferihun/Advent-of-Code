namespace Advent_of_Code_4
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
            List<string> lines = Read();
            int points = 0;

            foreach (string line in lines)
            {
                string cards = line.Split(':')[1];
                string wc = cards.Split('|')[0];
                string mc = cards.Split('|')[1];

                string[] wCards = wc.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] mCards = mc.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                List<int> winningCards = wCards.Select(int.Parse).ToList();
                List<int> myCards = mCards.Select(int.Parse).ToList();

                int power = -1;
                int point = 2;

                var result = myCards.Intersect(winningCards);
                power += result.Count();

                if (power != -1)
                {
                    points += Convert.ToInt32(Math.Pow(Convert.ToDouble(point), Convert.ToDouble(power)));
                }
            }

            return points;
        }

        static int Part2()
        {
            List<string> lines = Read();
            int scratchAmount = 0;
            Dictionary<int, int> scratchCards = new Dictionary<int, int>();

            for (int i = 1; i <= lines.Count; i++)
            {
                string line = lines[i - 1];

                int id = int.Parse(line.Split(':')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
                string cards = line.Split(':')[1];
                string wc = cards.Split('|')[0];
                string mc = cards.Split('|')[1];

                string[] wCards = wc.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] mCards = mc.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                List<int> winningCards = wCards.Select(int.Parse).ToList();
                List<int> myCards = mCards.Select(int.Parse).ToList();

                var followingScratches = myCards.Intersect(winningCards);


                if (scratchCards.ContainsKey(i))
                {
                    scratchCards[i] += 1;
                }
                else
                {
                    scratchCards[i] = 1;
                }

                if (followingScratches.Count() > 0)
                {
                    int increase = 1;
                    while (increase <= followingScratches.Count())
                    {
                        if (scratchCards.ContainsKey(i + increase))
                        {
                            scratchCards[i + increase] += scratchCards[i];
                        }
                        else
                        {
                            scratchCards[i + increase] = scratchCards[i];
                        }

                        increase++;
                    }
                }
            }

            foreach (KeyValuePair<int, int> card in scratchCards)
            {
                scratchAmount += card.Value;
            }

            return scratchAmount;
        }
    }
}