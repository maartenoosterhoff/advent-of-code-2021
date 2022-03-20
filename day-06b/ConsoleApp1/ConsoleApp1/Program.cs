// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt");
var lanternFish = data.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
var daysLeft = 256;

var lanternFishCount = new long[9];
foreach (var fish in lanternFish)
{
    lanternFishCount[fish]++;
}

while (daysLeft > 0)
{
    var newLanternFish = lanternFishCount[0];
    for (var i = 1; i <= 8; i++)
    {
        lanternFishCount[i - 1] = lanternFishCount[i];
    }
    lanternFishCount[6] += newLanternFish;
    lanternFishCount[8] = newLanternFish;

    daysLeft--;
}

Console.WriteLine("Solution: {0}", lanternFishCount.Sum());
