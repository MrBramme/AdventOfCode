using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Core;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202002 : ISolution
    {
        private readonly ILogger<Solution202002> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day02.txt";

        public Solution202002(ILogger<Solution202002> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var passwords = _inputService.GetInput(resourceLocation);
            var passwordValidator = new PasswordValidator202002();
            var result = 0;
            foreach (var password in passwords)
            {
                if (passwordValidator.IsValid(password))
                {
                    result += 1;
                }
            }

            return $"{result}";
        }
    }
}