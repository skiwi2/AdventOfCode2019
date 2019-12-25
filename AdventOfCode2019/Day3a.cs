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
            var cells = new Dictionary<(int, int), HashSet<int>>();
            var rawPaths = File.ReadAllLines("day3.txt");

            DrawWire(cells, 0, rawPaths[0].Split(','));
            DrawWire(cells, 1, rawPaths[1].Split(','));

            return FindEligibleCrossings(cells, rawPaths.Length)
                .Select(t => CalculateManhattanDistanceFromCentralPort(t.Item1, t.Item2))
                .Min();
        }

        void DrawWire(IDictionary<(int, int), HashSet<int>> cells, int wire, IEnumerable<string> path)
        {
            void AddCell(int xx, int yy)
            {
                if (cells.TryGetValue((xx, yy), out var set))
                {
                    set.Add(wire);
                }
                else
                {
                    cells[(xx, yy)] = new HashSet<int> { wire };

                }
            }

            var x = 0;
            var y = 0;
            AddCell(x, y);
            foreach (var move in path)
            {
                var direction = move[0];
                var number = int.Parse(move.Substring(1));
                switch (direction)
                {
                    case 'R':
                        for (int i = 0; i < number; i++)
                        {
                            AddCell(++x, y);
                        }
                        break;
                    case 'U':
                        for (int i = 0; i < number; i++)
                        {
                            AddCell(x, ++y);
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < number; i++)
                        {
                            AddCell(--x, y);
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < number; i++)
                        {
                            AddCell(x, --y);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown direction: {direction}");
                }
            }
        }

        IEnumerable<(int, int)> FindEligibleCrossings(IDictionary<(int, int), HashSet<int>> cells, int wires)
        {
            return cells
                .Where(kvp => kvp.Key.Item1 != 0 && kvp.Key.Item2 != 0)
                .Where(kvp => kvp.Value.Count == wires)
                .Select(kvp => kvp.Key);
        }

        int CalculateManhattanDistanceFromCentralPort(int x, int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }
    }
}
