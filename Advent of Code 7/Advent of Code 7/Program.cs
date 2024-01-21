namespace Advent_of_Code_7;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Part 1 solution: " + Part1());
    }

    static List<string> Read()
    {
        return File.ReadAllLines("input.txt").ToList();
    }

    static long Part1()
    {
        List<string> lines = Read();
        Dictionary<string, long> cardBids = new Dictionary<string, long>();

        foreach (var line in lines)
        {
            cardBids[line.Split(" ")[0]] = long.Parse(line.Split(" ")[1]);
        }

        Dictionary<string, long> cardPoints = AssignRanks(cardBids.Keys.ToList());

        foreach (var card in cardPoints)
        {
            Console.WriteLine(card.Key + " " + card.Value);
        }

        long totalWinnings = cardBids.Keys.ToList().Zip(cardBids.Values.ToList(), (hand, bid) => cardPoints[hand] * bid)
            .Sum();

        return totalWinnings;
    }

    static Dictionary<string, long> AssignRanks(List<string> hands)
    {
        var sortedHands = hands.OrderByDescending(hand => HandRank(hand))
                              .ThenByDescending(hand => HandValues(hand))
                              .ToList();

        var ranks = new Dictionary<string, long>();
        long rank = 1;

        foreach (var hand in sortedHands)
        {
            ranks[hand] = rank++;
        }

        return ranks;
    }

    static long HandRank(string hand)
    {
        if (IsFiveOfAKind(hand))
            return 6000;
        else if (IsFourOfAKind(hand))
            return 5000;
        else if (IsFullHouse(hand))
            return 4000;
        else if (IsThreeOfAKind(hand))
            return 3000;
        else if (IsTwoPair(hand))
            return 2000;
        else if (IsOnePair(hand))
            return 1000;
        else
            return 1;
    }

    static long HandValues(char card)
    {
        Dictionary<char, long> values = new Dictionary<char, long>
        {
            { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J', 11 }, { 'T', 10 },
            { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 },
            { '4', 4 }, { '3', 3 }, { '2', 2 }
        };

        return hand.Select(card => values[card]).Max();
    }

    static bool IsFiveOfAKind(string hand)
    {
        return hand.Distinct().Count() == 1;
    }

    static bool IsFourOfAKind(string hand)
    {
        var counts = hand.GroupBy(c => c).Select(group => group.Count());
        return counts.Any(count => count == 4);
    }

    static bool IsFullHouse(string hand)
    {
        var counts = hand.GroupBy(c => c).Select(group => group.Count());
        return counts.Contains(3) && counts.Contains(2);
    }

    static bool IsThreeOfAKind(string hand)
    {
        var counts = hand.GroupBy(c => c).Select(group => group.Count());
        return counts.Contains(3);
    }

    static bool IsTwoPair(string hand)
    {
        var counts = hand.GroupBy(c => c).Select(group => group.Count());
        return counts.Count(count => count == 2) == 2;
    }

    static bool IsOnePair(string hand)
    {
        var counts = hand.GroupBy(c => c).Select(group => group.Count());
        return counts.Count(count => count == 2) == 1;
    }
}




