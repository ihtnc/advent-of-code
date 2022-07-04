namespace AdventOfCode.Shared;

public interface ISolutionRunner
{
    object Run(RunOption option);

    string Describe(DescribeOption option);
}

public class SolutionRunner : ISolutionRunner
{
    private IEnumerable<Type> _contracts;

    public SolutionRunner()
    {
        var type = typeof(IProblem);

        var contracts = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsAbstract is false
                && t.GetConstructor(Type.EmptyTypes) is not null
                && t.GetCustomAttributes(typeof(ProblemAttribute), true).Any()
                && type.IsAssignableFrom(t)) ?? new Type[0];

        _contracts = contracts;
    }

    public object Run(RunOption option)
    {
        var problem = GetProblem(option.Year, option.Day);
        if (problem is null)
        {
            var error = GetRunErrorMessage(option, "Problem doesn't exist or solution not found yet.");
            throw new ArgumentException(error);
        }

        return problem.Solve(option);
    }

    public string Describe(DescribeOption option)
    {
        var problem = GetProblem(option.Year, option.Day);
        if (problem is null)
        {
            var error = GetDescribeErrorMessage(option, "Problem doesn't exist or solution not found yet.");
            throw new ArgumentException(error);
        }

        return
@$"Problem Url: {problem.Url}

{problem.GetDescription(option)}";
    }

    private IProblem? GetProblem(int year, int day)
    {
        var contract = _contracts.FirstOrDefault(s =>
        {
            var attr = s.GetCustomAttributes(typeof(ProblemAttribute), true).FirstOrDefault() as ProblemAttribute;
            return attr?.Year == year && attr?.Day == day;
        });

        if(contract is null) { return null;}

        return Activator.CreateInstance(contract) as IProblem;
    }

    private string GetRunErrorMessage(RunOption option, string message)
    {
        var header = $"Error running solution for year {option.Year} day {option.Day} problem.";
        return $"{header}: {message}";
    }

    private string GetDescribeErrorMessage(DescribeOption option, string message)
    {
        var header = $"Error describing year {option.Year} day {option.Day} problem.";
        return $"{header}: {message}";
    }
}