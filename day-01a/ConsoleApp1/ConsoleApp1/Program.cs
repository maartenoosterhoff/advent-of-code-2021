// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;

var count = 0;
var lines = File.ReadAllLines("input.txt");
if (lines.Length > 1)
{
    var current = -1;
    foreach (var line in lines)
    {
        if (current == -1)
        {
            current = int.Parse(line);
        }
        else
        {
            var prev = current;
            current = int.Parse(line);
            if (current > prev)
            {
                count++;
            }
        }
    }
}

Console.WriteLine("Solution: {0}", count);
