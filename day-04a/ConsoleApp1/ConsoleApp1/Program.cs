// See https://aka.ms/new-console-template for more information

var data = File.ReadAllLines("input.txt");

var moves = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
var pos = 2;
var bingoCards = new List<BingoCard>();
while (data.Length >= pos)
{
    bingoCards.Add(BingoCard.Parse(data.Skip(pos).Take(5).ToArray()));
    pos += 6;
}

var moveIdx = 0;
while (!bingoCards.Any(x => x.HasBingo()))
{
    foreach (var bingoCard in bingoCards)
    {
        bingoCard.Scratch(moves[moveIdx]);
    }

    moveIdx++;
}

var winningBingoCard = bingoCards.First(x => x.HasBingo());
var score = winningBingoCard.CalculateScore();
Console.WriteLine("Solution: {0}", score);



public class BingoCard
{
    private readonly List<List<Entry>> _data;
    private readonly int _dimension;

    private int _lastScratch = -1;

    private class Entry
    {
        public Entry(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public bool IsScratched { get; set; }
    }

    public static BingoCard Parse(string[] data)
    {
        var numbers = data
            .Select(x => x
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList()
            )
            .ToList();
        return new BingoCard(numbers);
    }

    private BingoCard(List<List<int>> data)
    {
        foreach (var items in data)
        {
            if (items.Count != data.Count)
            {
                throw new ArgumentException("Data is invalid.", nameof(data));
            }
        }

        _data = data
            .Select(x => x.Select(y => new Entry(y)).ToList())
            .ToList();
        _dimension = data.Count;
    }

    public void Scratch(int value)
    {
        foreach (var items in _data)
        {
            foreach (var item in items)
            {
                if (item.Value == value)
                {
                    item.IsScratched = true;
                    _lastScratch = value;
                }
            }
        }
    }

    public bool HasBingo()
    {
        // Rows
        if (_data.Any(x => x.All(y => y.IsScratched)))
        {
            return true;
        }

        // Columns
        for (var i = 0; i < _dimension; i++)
        {
            if (_data.All(x => x[i].IsScratched))
            {
                return true;
            }
        }

        // Diagonal
        var isDiagonal = true;
        for (var i = 0; i < _dimension; i++)
        {
            if (!_data[i][i].IsScratched ||
                !_data[i][_dimension - i - 1].IsScratched
            )
            {
                isDiagonal = false;
                break;
            }
        }

        return isDiagonal;
    }

    public int CalculateScore()
    {
        return _data.Sum(x => x.Where(y => !y.IsScratched).Sum(y => y.Value)) * _lastScratch;
    }
}