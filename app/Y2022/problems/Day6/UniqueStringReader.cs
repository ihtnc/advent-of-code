using System.Text;

namespace AdventOfCode.App.Y2022.Problems.Day6;

public class UniqueStringReader
{
    private int _totalCharacters;
    private int _uniqueCount;
    private Queue<char> _text;
    private Queue<char> _currentUnique;
    private HashSet<char> _uniqueLookup;

    public UniqueStringReader(int uniqueCount)
    {
        _uniqueCount = uniqueCount;
        _text = new Queue<char>();
        _currentUnique = new Queue<char>();
        _uniqueLookup = new HashSet<char>();
        _totalCharacters = 0;
    }

    public int Length => _text.Count;

    public int UniqueTextIndex => _totalCharacters - _currentUnique.Count;

    public string GetText() => ReadQueue(_text);

    public string GetUniqueString() => ReadQueue(_currentUnique);

    private string ReadQueue(Queue<char> value)
    {
        var output = new StringBuilder();
        var totalCharacters = value.Count;
        var current = 0;

        while(current < totalCharacters)
        {
            var c = _currentUnique.Dequeue();
            output.Append(c);
            value.Enqueue(c);
            current++;
        }

        return output.ToString();
    }

    public bool Add(char c)
    {
        _text.Enqueue(c);

        if (_uniqueLookup.Count == _uniqueCount) { return false; }

        _currentUnique.Enqueue(c);
        if (_uniqueLookup.Contains(c))
        {
            var removed = _currentUnique.Dequeue();
            while(removed != c)
            {
                _uniqueLookup.Remove(removed);
                removed = _currentUnique.Dequeue();
            }
        }
        else
        {
            _uniqueLookup.Add(c);
        }

        _totalCharacters++;
        return true;
    }

    public static UniqueStringReader Create(int uniqueCount) => new UniqueStringReader(uniqueCount);
}