using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day1a : IRunnable
    {
        public int Run()
        {
            return File.ReadAllLines("day1.txt")
                .Select(line => int.Parse(line))
                .Select(x => x / 3)
                .Select(x => x - 2)
                .Sum();
        }
    }
}
