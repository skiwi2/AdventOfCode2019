using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day2a : IRunnable
    {
        public int Run()
        {
            var opcodes = File.ReadAllText("day2.txt").Split(',').Select(str => int.Parse(str)).ToArray();
            opcodes[1] = 12;
            opcodes[2] = 2;
            var @continue = true;
            int i = 0;
            while (@continue)
            {
                switch (opcodes[i])
                {
                    case 1:
                        opcodes[opcodes[i + 3]] = opcodes[opcodes[i + 1]] + opcodes[opcodes[i + 2]];
                        break;
                    case 2:
                        opcodes[opcodes[i + 3]] = opcodes[opcodes[i + 1]] * opcodes[opcodes[i + 2]];
                        break;
                    case 99:
                        @continue = false;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown opcode");
                }
                i += 4;
            }
            return opcodes[0];
        }
    }
}
