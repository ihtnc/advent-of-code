namespace AdventOfCode.Shared.Utilities;

public delegate bool ValueConverterDelegate<T>(string? value, out T converted);