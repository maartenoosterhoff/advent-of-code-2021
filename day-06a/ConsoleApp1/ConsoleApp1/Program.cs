// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt");
var lanternFish = data.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
var daysLeft = 80;

while (daysLeft > 0)
{
    var newLanternFish = new List<int>();
    var count = lanternFish.Count;
    for (var i = 0; i < count; i++)
    {
        if (lanternFish[i] == 0)
        {
            lanternFish[i] = 6;
            newLanternFish.Add(8);
        }
        else
        {
            lanternFish[i]--;
        }
    }
    lanternFish.AddRange(newLanternFish);
    daysLeft--;
}

Console.WriteLine("Solution: {0}", lanternFish.Count);
