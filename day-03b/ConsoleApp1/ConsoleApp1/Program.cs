// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt");

string oxygen = string.Empty;
{
    var pos = 0;
    var items = data.ToList();
    while (items.Count > 1)
    {
        var count = items.Count;
        var count1s = items
            .Select(x => x.Substring(pos, 1))
            .Count(x => x == "1");
        var count0s = count - count1s;
        if (count0s == count1s)
        {
            items = items.Where(x => x.Substring(pos, 1) == "1").ToList();
        }
        else
        {
            var v = count1s > count0s ? "1" : "0";
            items = items.Where(x => x.Substring(pos, 1) == v).ToList();
        }

        pos++;
    }
    oxygen = items[0];
}

var co2 = string.Empty;
{
    var pos = 0;
    var items = data.ToList();
    while (items.Count > 1)
    {
        var count = items.Count;
        var count1s = items
            .Select(x => x.Substring(pos, 1))
            .Count(x => x == "1");
        var count0s = count - count1s;
        if (count0s == count1s)
        {
            items = items.Where(x => x.Substring(pos, 1) == "0").ToList();
        }
        else
        {
            var v = count1s > count0s ? "0" : "1";
            items = items.Where(x => x.Substring(pos, 1) == v).ToList();
        }
        pos++;
    }
    co2 = items[0];
}
var oxygenv = Convert.ToInt32(oxygen, 2);
var co2v = Convert.ToInt32(co2, 2);

Console.WriteLine("Solution: {0}", oxygenv * co2v);
