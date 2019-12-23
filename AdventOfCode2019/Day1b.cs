using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day1b : IRunnable
    {
        public int Run()
        {
            return File.ReadAllLines("day1.txt")
                .Select(line => int.Parse(line))
                .Select(x => TotalFuel(x))
                .Sum();
        }

        private static int TotalFuel(int mass)
        {
            var fuel = (mass / 3) - 2;
            if (fuel <= 0)
            {
                return 0;
            }
            else
            {
                return fuel + TotalFuel(fuel);
            }
        }
    }
}
