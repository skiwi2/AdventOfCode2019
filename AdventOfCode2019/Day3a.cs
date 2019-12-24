using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day3a : IRunnable
    {
        public int Run()
        {
            var zeroOffset = 15000;
            var size = 30000;
            var wires = 2;
            var cells = new bool[size, size, wires];
            var rawPaths = File.ReadAllLines("day3.txt");

            DrawWire(ref cells, zeroOffset, 0, rawPaths[0].Split(','));
            DrawWire(ref cells, zeroOffset, 1, rawPaths[1].Split(','));

            return FindEligibleCrossings(ref cells, wires, size, zeroOffset)
                .Select(t => CalculateManhattanDistanceFromCentralPort(t.Item1, t.Item2))
                .Min();
        }

        void DrawWire(ref bool[,,] cells, int zeroOffset, int wire, IEnumerable<string> path)
        {
            var x = 0;
            var y = 0;
            cells[zeroOffset + x, zeroOffset + y, wire] = true;
            foreach (var move in path)
            {
                var direction = move[0];
                var number = int.Parse(move.Substring(1));
                switch (direction)
                {
                    case 'R':
                        for (int i = 0; i < number; i++)
                        {
                            cells[zeroOffset + ++x, zeroOffset + y, wire] = true;
                        }
                        break;
                    case 'U':
                        for (int i = 0; i < number; i++)
                        {
                            cells[zeroOffset + x, zeroOffset + ++y, wire] = true;
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < number; i++)
                        {
                            cells[zeroOffset + --x, zeroOffset + y, wire] = true;
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < number; i++)
                        {
                            cells[zeroOffset + x, zeroOffset + --y, wire] = true;
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown direction: {direction}");
                }
            }
        }

        IList<(int, int)> FindEligibleCrossings(ref bool[,,] cells, int wires, int size, int zeroOffset)
        {
            var crossings = new List<(int, int)>();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x == zeroOffset && y == zeroOffset)
                    {
                        continue;
                    }
                    bool allWiresCross = true;
                    for (int z = 0; z < wires; z++)
                    {
                        if (!cells[x, y, z])
                        {
                            allWiresCross = false;
                        }
                    }
                    if (allWiresCross)
                    {
                        crossings.Add((x - zeroOffset, y - zeroOffset));
                    }
                }
            }
            return crossings;
        }

        int CalculateManhattanDistanceFromCentralPort(int x, int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }
    }
}
