// See https://aka.ms/new-console-template for more information

using System.ComponentModel;

var data = File.ReadAllLines("testinput.txt").Select(x => new Item(x)).ToList();
foreach (var item in data)
{
    var decoder = new Decoders();
    foreach (var pattern in item.SignalPatterns.OrderBy(x => x.Length))
    {
        decoder.AddSignalPattern(pattern);
    }

    var translation = decoder.GetTranslation();
}


Console.WriteLine("Solution: {0}", 0);

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

public class Decoders
{
    private Dictionary<string, List<string>> _candidates;

    private readonly List<string> _allCandidates;

    public Decoders()
    {
        _allCandidates = "abcdefg".Select(x => x.ToString()).ToList();
        _candidates = _allCandidates.ToDictionary(
            x => x,
            _ => _allCandidates.Select(z => z.ToString()).ToList());
    }

    private Dictionary<int, string> _patternHandling = new Dictionary<int, string>
    {
        { 2, "cf" },        // 1
        { 4, "bcdf" },      // 4
        { 3, "acf" },       // 7
        //{ 7, "abcdefg" },   // 8 -> there are no unused position, so no candidate removal
        //{ 5, "abcdefg" },   // 2, 3, and 5 -> there are no unused position, so no candidate removal
        //{ 6, "abcdefg"}     // 0, 6 and 9 -> there are no unused position, so no candidate removal
    };

    public void AddSignalPattern(string pattern)
    {
        if (_patternHandling.ContainsKey(pattern.Length))
        {
            var removalExclusions = _patternHandling[pattern.Length];
            foreach (var candidate in _allCandidates)
            {
                if (removalExclusions.Contains(candidate))
                {
                    // Only allow candidates
                    _candidates[candidate].RemoveAll(x => !pattern.Contains(x));
                }
                else
                {
                    // Remove candidates
                    _candidates[candidate].RemoveAll(pattern.Contains);
                }
            }
        }

        // rule: 1 and 0 share 2 leds
        // rule: 1 and 6 share 1 led
        // rule: 1 and 9 share 2 leds
        // rule: candidate which is present for both 1 and 6 => f
        if (pattern.Length == 6)
        {
            // 0/6/9
            // we know length 2 (=value 1) has already passed, has candidates cf
            var cand1 = _candidates["c"][0];
            var cand2 = _candidates["c"][1];
            if (!(pattern.Contains(cand1) && pattern.Contains(cand2)))
            {
                if (!pattern.Contains(cand1))
                {
                    _candidates["c"].Remove(cand2);
                }
                if (!pattern.Contains(cand2))
                {
                    _candidates["c"].Remove(cand1);
                }
            }

        }

        var changed = true;
        while (changed)
        {
            changed = false;
            foreach (var candidate in _allCandidates)
            {
                if (_candidates[candidate].Count == 1)
                {
                    foreach (var otherCandidate in _allCandidates)
                    {
                        if (otherCandidate == candidate ||
                            !_candidates[otherCandidate].Contains(_candidates[candidate].Single())
                            )
                        {
                            continue;
                        }

                        _candidates[otherCandidate].Remove(_candidates[candidate].Single());
                        changed = true;
                    }
                }
            }
        }
    }

    public Dictionary<string, string> GetTranslation()
    {
        if (_candidates.Any(x => x.Value.Count != 1))
        {
            throw new InvalidOperationException();
        }

        return null;
    }
}