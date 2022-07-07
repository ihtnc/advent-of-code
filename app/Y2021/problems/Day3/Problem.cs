using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2021.Problems.Day3;

[Problem(Year = 2021, Day = 3)]
public class Problem : IProblem
{
    private static readonly Regex _inlineInputFormat = new Regex(@"((?<=^)|(?<=\s))(?'value'[10]+)((?=\s)|(?=$))");

    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    public Problem() : this(new TextHelper(), new FileHelper())
    { }

    public Problem(ITextHelper textHelper, IFileHelper filehelper)
    {
        _textHelper = textHelper;
        _fileHelper = filehelper;
    }

    public string Url => "https://adventofcode.com/2021/day/3";

    public object Solve(RunOption option)
    {
        var input = $"{option.Input}";

        IEnumerable<string> binaryValues;
        if (IsInlineInput(input))
        {
            binaryValues = _textHelper.ParseInput<string>(input, InputValueConverter, LineValueConverter);
        }
        else
        {
            binaryValues = _fileHelper.ParseLines<string>(input, LineValueConverter);
        }

        var values = ConvertToInt(binaryValues);

        switch (option?.Part)
        {
            case 1: return GetPowerConsumption(values);
            case 2: return GetLifeSupportRating(values);
            default: return 0;
        }
    }

    public Description GetDescription(DescribeOption option)
    {
        switch (option.Part)
        {
            case 1:
                return new Part1Description();
            case 2:
                return new Part2Description();
            default:
                return new ErrorDescription($"Problem has no part {option.Part}.");
      }
    }

    public static bool IsInlineInput(string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public static bool LineValueConverter(string? value, out string converted)
    {
        converted = string.Empty;

        if (string.IsNullOrWhiteSpace(value)) { return false; }
        if (IsInlineInput(value) is false) { return false; }

        converted = value;
        return true;
    }

    public static IEnumerable<string> InputValueConverter(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            var value =  match.Groups["value"].Value;
            yield return value;

            match = match.NextMatch();
        }
    }

    private static IEnumerable<int> ConvertToInt(IEnumerable<string> values)
    {
        foreach(var value in values)
        {
            yield return Convert.ToInt32($"{value}", 2);
        }
    }

    private static IEnumerable<string> ConvertToBinary(IEnumerable<int> values)
    {
        foreach(var value in values)
        {
            yield return Convert.ToString(value, 2);
        }
    }

    private static IEnumerable<string> NormaliseLength(IEnumerable<string> values)
    {
        var maxLength = 0;
        foreach(var value in values)
        {
            maxLength = Math.Max(maxLength, value.Length);
        }

        var normalisedLength = NormaliseItemLength(values, maxLength);
        return normalisedLength;
    }

    private static IEnumerable<string> NormaliseItemLength(IEnumerable<string> value, int maxLength)
    {
        foreach(var item in value)
        {
            var normalisedItem = item.PadLeft(maxLength, '0');
            yield return normalisedItem;
        }
    }

    public static long GetPowerConsumption(IEnumerable<int> input)
    {
        var values = ConvertToBinary(input);
        values = NormaliseLength(values);
        var valueCount = values.Count();
        var bits = GetBitCounts(values);
        var gammaValue = new StringBuilder();
        var epsilonValue = new StringBuilder();

        for(var i = 0; i < bits.Count; i++)
        {
            var activeCount = bits[i];
            var inactiveCount = valueCount - activeCount;
            var gammaBit = inactiveCount <= activeCount ? '1' : '0';
            var epsilonBit = inactiveCount < activeCount ? '0' : '1';
            gammaValue.Insert(0, gammaBit);
            epsilonValue.Insert(0, epsilonBit);
        }

        var gamma = Convert.ToInt32($"{gammaValue}", 2);
        var epsilon = Convert.ToInt32($"{epsilonValue}", 2);

        return gamma * epsilon;
    }

    public static long GetLifeSupportRating(IEnumerable<int> input)
    {
        var generatorRating = GetOxygenGeneratorRating(input);
        var scrubberRating = GetCarbonDioxideScrubberRating(input);
        return generatorRating * scrubberRating;
    }

    public static int GetOxygenGeneratorRating(IEnumerable<int> input)
    {
        var values = ConvertToBinary(input);
        var ratings = NormaliseLength(values);
        var bitIndex = 0;

        while (ratings.Count() > 1)
        {
            ratings = GetValuesWithCommonBits(ratings, bitIndex);
            // remove duplicates, otherwise we might get into an infinite loop
            ratings = new HashSet<string>(ratings);
            bitIndex++;
        }

        var rating = Convert.ToInt32($"{ratings.FirstOrDefault() ?? "0"}", 2);
        return rating;
    }

    public static int GetCarbonDioxideScrubberRating(IEnumerable<int> input)
    {
        var values = ConvertToBinary(input);
        var ratings = NormaliseLength(values);
        var bitIndex = 0;

        while (ratings.Count() > 1)
        {
            ratings = GetValuesWithCommonBits(ratings, bitIndex, invertSelection: true);
            // remove duplicates, otherwise we might get into an infinite loop
            ratings = new HashSet<string>(ratings);
            bitIndex++;
        }

        var rating = Convert.ToInt32($"{ratings.FirstOrDefault() ?? "0"}", 2);
        return rating;
    }

    private static IEnumerable<string> GetValuesWithCommonBits(IEnumerable<string> values, int bitIndex, bool invertSelection = false)
    {
        var bit1Items = new List<string>();
        var bit0Items = new List<string>();
        foreach (var value in values)
        {
            if (bitIndex < 0 || bitIndex >= value.Length) { continue; }

            var list = value[bitIndex] == '1' ? bit1Items : bit0Items;
            list.Add(value);
        }

        var commonItems = bit1Items.Count >= bit0Items.Count ? bit1Items : bit0Items;
        var uncommonItems = bit1Items.Count >= bit0Items.Count ? bit0Items : bit1Items;
        var result = invertSelection ? uncommonItems : commonItems;
        return result;
    }

    private static Dictionary<int, int> GetBitCounts(IEnumerable<string> values)
    {
        var bitCounter = new Dictionary<int, int>();

        foreach(var value in values)
        {
            if (string.IsNullOrWhiteSpace(value)) { return new Dictionary<int, int>(); }

            for(var i = 0; i < value.Length; i++)
            {
                if(bitCounter.ContainsKey(i) is false) { bitCounter.Add(i, 0); }

                var current = (value.Length - 1) - i;
                var bit = value[current];

                switch (bit)
                {
                    case '1':
                        bitCounter[i]++;
                        break;
                }
            }
        }

        return bitCounter;
    }
}