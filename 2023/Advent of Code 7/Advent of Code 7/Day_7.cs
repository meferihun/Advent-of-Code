namespace Advent_of_Code_7;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Part 1 solution: " + Part1()); }

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

        long totalWinnings = cardBids.Keys.ToList().Zip(cardBids.Values.ToList(), (hand, bid) => cardPoints[hand] * bid)
            .Sum();

        return totalWinnings;
    }
    
    static Dictionary<string, long> AssignRanks(List<string> hands)
    {
        var handPoints = new Dictionary<string, long>();

        foreach (var hand in hands)
        {
            handPoints[hand] = HandRank(hand);
        }

        var fiveKinds = new Dictionary<string, long>();
        var fourKinds = new Dictionary<string, long>();
        var fullHouse = new Dictionary<string, long>();
        var threeKinds = new Dictionary<string, long>();
        var twoPairs = new Dictionary<string, long>();
        var onePair = new Dictionary<string, long>();
        var noPairs = new Dictionary<string, long>();

        foreach (var hand in handPoints)
        {
            switch (hand.Value)
            {
                case 6000:
                    fiveKinds[hand.Key] = hand.Value;
                    break;
                case 5000:
                    fourKinds[hand.Key] = hand.Value;
                    break;
                case 4000:
                    fullHouse[hand.Key] = hand.Value;
                    break;
                case 3000:
                    threeKinds[hand.Key] = hand.Value;
                    break;
                case 2000:
                    twoPairs[hand.Key] = hand.Value;
                    break;
                case 1000:
                    onePair[hand.Key] = hand.Value;
                    break;
                case 1:
                    onePair[hand.Key] = hand.Value;
                    break;
            }
        }

        var kinds = new[] { fiveKinds, fourKinds, fullHouse, threeKinds, twoPairs, onePair, noPairs };

        SortRanks(ref kinds);

        fiveKinds = kinds[0];
        fourKinds = kinds[1];
        fullHouse = kinds[2];
        threeKinds = kinds[3];
        twoPairs = kinds[4];
        onePair = kinds[5];
        noPairs = kinds[6];

        var mergedHands = ForEachMethod(fiveKinds, fourKinds, fullHouse, threeKinds, twoPairs, onePair, noPairs);

        var sortedHands = from entry in mergedHands orderby entry.Value ascending select entry;

        var ranks = new Dictionary<string, long>();
        long rank = 1;

        foreach (var hand in sortedHands)
        {
            ranks[hand.Key] = rank++;
        }

        return ranks;
    }

    static void SortRanks(ref Dictionary<string, long>[] ranks)
    {
        foreach (var rank in ranks)
        {
            foreach (var hand in rank)
            {
                foreach (var otherHand in rank)
                {
                    if (hand.Key != otherHand.Key)
                    {
                        for (int i = 0; i < hand.Key.Length; i++)
                        {
                            if (hand.Key[i] != otherHand.Key[i])
                            {
                                rank[
                                    CardValue(hand.Key[i]) > CardValue(otherHand.Key[i]) ? hand.Key : otherHand.Key]++;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    static Dictionary<TKey, TValue> ForEachMethod<TKey, TValue>(
        params Dictionary<TKey, TValue>[] dictionaries)
    {
        var mergedDictionary = new Dictionary<TKey, TValue>();

        foreach (var dictionary in dictionaries)
        {
            foreach (var kvp in dictionary)
            {
                if (!mergedDictionary.ContainsKey(kvp.Key))
                {
                    mergedDictionary[kvp.Key] = kvp.Value;
                }
            }
        }

        return mergedDictionary;
    }

    static long HandRank(string hand)
    {
        if (IsFiveOfAKind(hand))
            return 6000;
        if (IsFourOfAKind(hand))
            return 5000;
        if (IsFullHouse(hand))
            return 4000;
        if (IsThreeOfAKind(hand))
            return 3000;
        if (IsTwoPair(hand))
            return 2000;
        if (IsOnePair(hand))
            return 1000;
        return 1;
    }

    static int CardValue(char card)
    {
        Dictionary<char, int> values = new Dictionary<char, int>
        {
            { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J', 11 }, { 'T', 10 },
            { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 },
            { '4', 4 }, { '3', 3 }, { '2', 2 }
        };

        return values.FirstOrDefault(value => value.Key == card).Value;
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