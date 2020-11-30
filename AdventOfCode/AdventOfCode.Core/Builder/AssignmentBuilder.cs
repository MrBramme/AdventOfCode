using AdventOfCode.Domain.Domain;
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace AdventOfCode.Core.Builder
{
    public class AssignmentBuilder : IAssignmentBuilder
    {
        private readonly ILogger<AssignmentBuilder> _logger;

        public AssignmentBuilder(ILogger<AssignmentBuilder> logger)
        {
            _logger = logger;
        }
        public Assignment CreateAssignment(string assignment)
        {
            try
            {
                var year = int.Parse(assignment.Substring(0, 4));
                var day = int.Parse(assignment.Substring(4, 2));
                var suffix = assignment.Substring(6);
                return new Assignment
                {
                    Year = year,
                    Day = day,
                    Suffix = suffix,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong while parsing the assignment string: '{assignment}'");
                throw;
            }
        }
    }
}