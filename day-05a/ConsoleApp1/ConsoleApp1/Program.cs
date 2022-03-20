// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt");
var lines = data.Select(x => new Line(x)).ToList();
var max = lines.Max(x => Max(x.Begin.X, x.Begin.Y, x.End.X, x.End.Y)) + 1;
var field = new Field(max);

foreach (var line in lines)
{
    field.MarkLine(line);
}

var pointsWithTwoPointOverlap = field.Count(2);

Console.WriteLine("Solution: {0}", pointsWithTwoPointOverlap);

int Max(params int[] values)
{
    return values.Max();
}

public class Field
{
    private readonly string[] _field;
    private readonly int _max;

    public Field(int max)
    {
        _max = max;
        _field = Enumerable.Range(0, max).Select(_ => new string('0', max)).ToArray();
    }

    public void MarkLine(Line line)
    {
        foreach (var point in line.GetPoints())
        {
            var fieldLine = _field[point.Y].ToCharArray();
            var posValue = int.Parse(fieldLine[point.X].ToString());
            posValue++;
            fieldLine[point.X] = posValue.ToString()[0];
            _field[point.Y] = new string(fieldLine);
        }
    }

    public int Count(int minimumValue)
    {
        return _field.Sum(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).Count(y => y >= minimumValue));
    }
}

public class Line
{
    public Point Begin { get; }
    public Point End { get; }

    public Line(string data)
    {
        const string marker = " -> ";
        var points = data.Replace(marker, ",").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        Begin = new Point(points[0], points[1]);
        End = new Point(points[2], points[3]);
    }

    public bool IsHorizontal => Begin.Y == End.Y;
    public bool IsVertical => Begin.X == End.X;

    public IEnumerable<Point> GetPoints()
    {
        if (IsHorizontal && !IsVertical)
        {
            var step = Begin.X < End.X ? 1 : -1;
            for (var x = Begin.X; x != End.X; x += step)
            {
                yield return new Point(x, Begin.Y);
            }
            yield return new Point(End.X, End.Y);
        }
        if (!IsHorizontal && IsVertical)
        {
            var step = Begin.Y < End.Y ? 1 : -1;
            for (var y = Begin.Y; y != End.Y; y += step)
            {
                yield return new Point(Begin.X, y);
            }
            yield return new Point(End.X, End.Y);
        }
    }
}

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
}