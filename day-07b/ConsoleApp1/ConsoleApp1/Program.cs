// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;

var data = File.ReadAllLines("input.txt");
var positions = data.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
var maxLeft = positions.Min();
var maxRight = positions.Max();
var fuelBurnCache = new ConcurrentDictionary<int, int>();

var minFuel = Enumerable.Range(maxLeft, maxRight - maxLeft + 1)
    .Select(desiredPosition => positions.Sum(x => GetFuelBurn(Math.Abs(x - desiredPosition), fuelBurnCache)))
    .Min();

Console.WriteLine("Solution: {0}", minFuel);

int GetFuelBurn(int step, ConcurrentDictionary<int, int> cache)
{
    if (step == 0)
    {
        return 0;
    }

    if (step == 1)
    {
        return 1;
    }

    return cache.GetOrAdd(
        step,
        _ => GetFuelBurn(step - 1, cache) + step);
}