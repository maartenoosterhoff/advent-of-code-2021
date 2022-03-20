// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

var data = File.ReadAllLines("input.txt");
var total = data.Length;
var d = data[0].Length;
var g = new int[d];
if (data.Length > 0)
{
    foreach (var item in data)
    {
        for (var i = 0; i < d; i++)
        {
            if (item[i] == '1')
            {
                g[i]++;
            }
        }
    }
}

var g2 = string.Empty;
var e2 = string.Empty;
for (var i = 0; i < d; i++)
{
    var o = g[i];
    var z = total - o;
    if (o > z)
    {
        g2 += "1";
        e2 += "0";
    }
    else
    {
        g2 += "0";
        e2 += "1";
    }
}
var gamma = Convert.ToInt32(g2, 2);
var epsilon= Convert.ToInt32(e2, 2);

Console.WriteLine("Solution: {0}", gamma * epsilon);
