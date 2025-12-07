using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;

class Program
{
    public static void Main()
    {
        var lines = Read();

        Console.WriteLine("Part 1 solution: " + Part1(lines));
        Console.WriteLine("Part 2 solution: " + Part2(lines));
    }

    public static int Part1(List<string> lines)
    {
        var startingPosition = 50;
        var numberOfZeros = 0;

        foreach (var line in lines)
        {
            startingPosition = HandleRotation(startingPosition, line).position;

            if (startingPosition == 0)
            {
                numberOfZeros++;
            }
        }

        return numberOfZeros;
    }

    public static int Part2(List<string> lines)
    {
        var startingPosition = 50;
        var numberOfZeros = 0;

        foreach (var line in lines)
        {
            var (position, zeroes) = HandleRotation(startingPosition, line);

            startingPosition = position;
            numberOfZeros += zeroes;
        }

        return numberOfZeros;
    }

    private static List<string> Read()
    {
        var fileName = "input.txt";
        return File.ReadAllLines(fileName).ToList();
    }

    private static (int position, int zeroes) HandleRotation(int currentPosition, string command)
    {
        var direction = command.First();
        var value = int.Parse(command[1..]);

        int firstK;
        if (direction == 'R')
            firstK = (100 - currentPosition) % 100;
        else
            firstK = currentPosition % 100;

        if (firstK == 0) firstK = 100;

        int numberOfZeroes = 0;
        if (value >= firstK)
        {
            numberOfZeroes = 1 + (value - firstK) / 100;
        }

        var newAbsolute = direction == 'R' ? currentPosition + value : currentPosition - value;
        var current = Mod(newAbsolute, 100);

        return (current, numberOfZeroes);
    }

    private static int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
}