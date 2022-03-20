// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt").Select(x => new Item(x)).ToList();
var countValues = new[] { 2, 4, 3, 7 };
var score = data.Sum(x => x.OutputValues.Count(z => countValues.Contains(z.Length)));

Console.WriteLine("Solution: {0}", score);

public class Item
{
    public List<string> SignalPatterns { get; }
    public List<string> OutputValues { get; }

    public Item(string lineDescription)
    {
        var blocks = lineDescription.Split('|', StringSplitOptions.RemoveEmptyEntries);
        SignalPatterns = blocks[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        OutputValues = blocks[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

        if (SignalPatterns.Count != 10 ||
            OutputValues.Count != 4)
        {
            throw new ArgumentException(nameof(lineDescription));
        }
    }
}