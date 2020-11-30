using AdventOfCode.Domain.Interfaces;
using System.IO;

namespace AdventOfCode.Core.Services
{
    public class InputService : IInputService
    {
        public string[] GetInput(string location)
        {
            var fullPath = System.Reflection.Assembly.GetCallingAssembly().Location;
            var resource = Path.Combine(Path.GetDirectoryName(fullPath), location);
            return File.ReadAllLines(resource);
        }
    }
}