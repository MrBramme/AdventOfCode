using AdventOfCode.Domain.Domain;

namespace AdventOfCode.Domain.Interfaces
{
    public interface ISolutionBuilder
    {
        ISolution CreateSolution(Assignment assignment);
    }
}