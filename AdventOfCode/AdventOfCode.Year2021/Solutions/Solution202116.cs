using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202116 : ISolution
    {
        private readonly ILogger<Solution202116> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day16.txt";

        public Solution202116(ILogger<Solution202116> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            throw new NotImplementedException("not done yet");
            var hex = _inputService.GetInput(resourceLocation)[0];
            var package = string.Join(string.Empty, hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            
            var result = GetPackageVersionTotal(package);
            return $"{result.versionTotal}";
        }

        private (int versionTotal, int lastIndexCount) GetPackageVersionTotal(string package)
        {
            var result = Convert.ToInt32(package[0..3], 2);
            var packageId = Convert.ToInt32(package[3..6], 2);
            var lastIndexCount = 6;
            if (packageId == 4)
            {
                var literalValue = ParseLiteralValue(package, 6);
                lastIndexCount += literalValue.Length;
                result += Convert.ToInt32(literalValue, 2);
            }
            else
            {
                var subPackageLengthBit = package[6] == '1' ? 11 : 15;
                lastIndexCount += 1 + subPackageLengthBit;
                var totalSubPackageCount = Convert.ToInt32(package[7..(7 + subPackageLengthBit)], 2); 
                var count = 0;
                while(count != totalSubPackageCount)
                {
                    var currentStart = lastIndexCount;
                    var (versionSum, lastIndex) = GetPackageVersionTotal(package[currentStart..]);
                    result += versionSum;
                    lastIndexCount += lastIndex;
                }
            }

            return (result, lastIndexCount);
        }

        private static string ParseLiteralValue(string package, int index)
        {
            var continueReading = true;
            var strWriter = new StringWriter();
            while (continueReading)
            {
                var part = package[index..(index + 5)];
                continueReading = part[0] == '1';
                strWriter.Write(part[1..5]);
                index += 5;
            }

            return strWriter.ToString();
        }
    }
}