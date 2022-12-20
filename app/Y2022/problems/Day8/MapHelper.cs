namespace AdventOfCode.App.Y2022.Problems.Day8;

public class MapHelper
{
    public static int[,] CreateValueMap(IEnumerable<string> input)
    {
        if (input.Any() is false) { return new int[0,0]; }

        input = NormaliseLength(input);
        var expectedLength = input.First().Length;
        var map = new int[input.Count(),expectedLength];

        var mapIndex = 0;
        foreach(var item in input)
        {
            if (string.IsNullOrEmpty(item)) { return new int[0,0]; }

            for(var i = 0; i < item.Length; i++)
            {
                var c = item[i];
                if (int.TryParse($"{c}", out var value) is false) { value = -1; }

                map[mapIndex, i] = value;
            }

            mapIndex++;
        }

        return map;
    }

    public static bool[,] CreateVisibilityMap(int[,] input)
    {
        var rowCount = input.GetLength(0);
        var colCount = input.GetLength(1);
        var visibilityMap = new bool[rowCount,colCount];
        for(var i = 0; i < rowCount; i++)
        {
            for(var j = 0; j < colCount; j++)
            {
                var maxHeight = input[i,j];
                var leftView = GetRow(input, i, count: j).Reverse();
                var rightView = GetRow(input, i, startColumn: j+1);
                var topView = GetColumn(input, j, count: i).Reverse();
                var bottomView = GetColumn(input, j, startRow: i+1);

                var left = CalculateVisibility(maxHeight, leftView);
                var right = CalculateVisibility(maxHeight, rightView);
                var top = CalculateVisibility(maxHeight, topView);
                var bottom = CalculateVisibility(maxHeight, bottomView);
                var visible = left || right || top || bottom;

                visibilityMap[i, j] = visible;
            }
        }

        return visibilityMap;
    }

    public static int[,] CreateScenicScoreMap(int[,] input)
    {
        var rowCount = input.GetLength(0);
        var colCount = input.GetLength(1);
        var scoreMap = new int[rowCount,colCount];
        for(var i = 0; i < rowCount; i++)
        {
            for(var j = 0; j < colCount; j++)
            {
                var maxHeight = input[i,j];
                var leftView = GetRow(input, i, count: j).Reverse();
                var rightView = GetRow(input, i, startColumn: j+1);
                var topView = GetColumn(input, j, count: i).Reverse();
                var bottomView = GetColumn(input, j, startRow: i+1);

                var leftScore = CalculateViewingDistance(maxHeight, leftView);
                var rightScore = CalculateViewingDistance(maxHeight, rightView);
                var topScore = CalculateViewingDistance(maxHeight, topView);
                var bottomScore = CalculateViewingDistance(maxHeight, bottomView);
                var scenicScore = leftScore * rightScore * topScore * bottomScore;

                scoreMap[i, j] = scenicScore;
            }
        }

        return scoreMap;
    }

    public static T[] GetRow<T>(T[,] table, int rowNumber, int startColumn = 0, int? count = null)
    {
        var rowCount = table.GetLength(0);
        var columnCount = table.GetLength(1);
        if(rowCount == 0 || count == 0) { return new T[0]; }
        if(rowNumber < 0 || rowNumber > rowCount) { return new T[0]; }
        if(startColumn < 0 || startColumn > columnCount) { return new T[0]; }

        var itemCount = count ?? columnCount - startColumn;
        var output = new T[itemCount];
        var outputIndex = 0;
        for(var i = startColumn; i < columnCount; i++)
        {
            if (outputIndex >= itemCount) { break; }

            output[outputIndex] = table[rowNumber, i];
            outputIndex++;
        }
        return output;
    }

    public static T[] GetColumn<T>(T[,] table, int columnNumber, int startRow = 0, int? count = null)
    {
        var rowCount = table.GetLength(0);
        var columnCount = table.GetLength(1);
        if(columnCount == 0 || count == 0) { return new T[0]; }
        if(columnNumber < 0 || columnNumber > columnCount) { return new T[0]; }
        if(startRow < 0 || startRow > rowCount) { return new T[0]; }

        var itemCount = count ?? rowCount - startRow;
        var output = new T[itemCount];
        var outputIndex = 0;
        for(var i = startRow; i < rowCount; i++)
        {
            if (outputIndex >= itemCount) { break; }

            output[outputIndex] = table[i, columnNumber];
            outputIndex++;
        }
        return output;
    }

    public static bool CalculateVisibility(int currentHeight, IEnumerable<int> view)
    {
        foreach(var item in view)
        {
            if (item >= currentHeight)
            {
                return false;
            }
        }

        return true;
    }

    public static int CalculateViewingDistance(int currentHeight, IEnumerable<int> view)
    {
        var output = 0;
        foreach(var item in view)
        {
            output++;

            if (item >= currentHeight)
            {
                break;
            }
        }

        return output;
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
            var normalisedItem = item.PadRight(maxLength, '0');
            yield return normalisedItem;
        }
    }
}