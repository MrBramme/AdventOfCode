using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202103Part2 : ISolution
    {
        private readonly ILogger<Solution202103Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day03.txt";

        public Solution202103Part2(ILogger<Solution202103Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var binaryLength = input[0].Length;

            var binaryCounter = input.Select(input => input.ToCharArray());

            var oxygenRating = -1;
            var oxygenRatingSelection = binaryCounter;
            for (var i = 0; i < binaryLength; i++)
            {
                oxygenRatingSelection = GetMostFrequent(oxygenRatingSelection, i, '1', true);
                if (oxygenRatingSelection.Count() == 1)
                {
                    oxygenRating = Convert.ToInt32(string.Join(string.Empty, oxygenRatingSelection.First()), 2);
                    continue;
                }
            }

            var co2Rating = -1;
            var co2RatingSelection = binaryCounter;
            for (var i = 0; i < binaryLength; i++)
            {
                co2RatingSelection = GetMostFrequent(co2RatingSelection, i, '0', false);
                if (co2RatingSelection.Count() == 1)
                {
                    co2Rating = Convert.ToInt32(string.Join(string.Empty, co2RatingSelection.First()), 2);
                    continue;
                }
            }
            
            var result = oxygenRating * co2Rating;
            return result.ToString();
        }

        private IEnumerable<char[]> GetMostFrequent(IEnumerable<char[]> input, int index, char tieBreaker, bool isMost = true)
        {
            var cntZero = 0;
            var cntOne = 0;
            foreach (var item in input)
            {
                if (item[index] == '1')
                {
                    cntOne++;
                } else
                {
                    cntZero++;
                }
            }
            var selector = tieBreaker;
            if (isMost)
            {
                if (cntZero > cntOne) selector = '0';
                if (cntOne > cntZero) selector = '1';
            } else
            {
                if (cntZero < cntOne) selector = '0';
                if (cntOne < cntZero) selector = '1';
            }
            
            return input.Where(x => x[index] == selector);
        }
    }
}