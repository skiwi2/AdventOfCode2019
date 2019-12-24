using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day2b : IRunnable
    {
        public int Run()
        {
            for (int n = 0; n < 100; n++)
            {
                for (int v = 0; v < 100; v++)
                {
                    var result = RunProgram(n, v);
                    if (result == 19690720)
                    {
                        return 100 * n + v;
                    }
                }
            }
            throw new InvalidOperationException("No matching noun and verb found");
        }

        int RunProgram(int noun, int verb)
        {
            var opcodes = File.ReadAllText("day2.txt").Split(',').Select(str => int.Parse(str)).ToArray();
            opcodes[1] = noun;
            opcodes[2] = verb;
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
