using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Parallels
{
    public class DelayedService
    {
        private readonly Random _randomizer = new Random();

        public async Task<int> GetData(string name)
        {
            //Debug.WriteLine(name);
            var delay = _randomizer.Next(2, 10);
            await Task.Delay(TimeSpan.FromSeconds(delay));
            return delay;
        }
    }
}