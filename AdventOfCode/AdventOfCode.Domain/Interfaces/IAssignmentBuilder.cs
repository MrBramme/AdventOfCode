using AdventOfCode.Domain.Domain;

namespace AdventOfCode.Domain.Interfaces
{
    public interface IAssignmentBuilder
    {
        Assignment CreateAssignment(string assignment);
    }
}