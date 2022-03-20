// See https://aka.ms/new-console-template for more information

var position = 0;
var depth = 0;
var data = File.ReadAllLines("input.txt");
if (data.Length > 0)
{
    foreach (var item in data)
    {
        var cmds = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (cmds.Length == 2)
        {
            var cmd = cmds[0];
            var value = int.Parse(cmds[1]);
            switch (cmd)
            {
                case "forward":
                    position += value;
                    break;
                case "down":
                    depth += value;
                    break;
                case "up":
                    depth -= value;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}

Console.WriteLine("Solution: {0}", position * depth);
