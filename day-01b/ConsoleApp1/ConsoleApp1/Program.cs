// See https://aka.ms/new-console-template for more information

var count = 0;
var data = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
if (data.Length > 3)
{
    var last4Values = data.Take(3).ToList();

    bool IsCurrentWindowLargerThanPrevWindow(IReadOnlyList<int> items)
    {
        if (items.Count != 4)
        {
            throw new InvalidOperationException();
        }

        return items[0] + items[1] + items[2] < items[1] + items[2] + items[3];
    }

    foreach (var item in data)
    {
        last4Values.Add(item);
        if (last4Values.Count > 4)
        {
            last4Values.RemoveAt(0);
        }
        if (IsCurrentWindowLargerThanPrevWindow(last4Values))
        {
            count++;
        }
    }
}

Console.WriteLine("Solution: {0}", count);
