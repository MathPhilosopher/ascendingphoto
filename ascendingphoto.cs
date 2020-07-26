using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* Problem: Ascending Photo
      For every person if person to the left is not right, we need a cut.
      this solution provides a correct answer for most cases, however,
      in some cases the solution is off by -1. If you have any comments
      regarding this problem please contribute.
    */

    static int Cut = 0;
    static readonly List<(int, int)> Lefts = new List<(int, int)>();
    static readonly List<int> HeightList = new List<int>();
    static IDictionary<int, int>
        Heights = new Dictionary<int, int>(),
        Sorted = new Dictionary<int, int>(),
        Locked = new Dictionary<int, int>();

    static void Main(string[] args)
    {
        Console.ReadLine();
        var line = Console.ReadLine().Split(" ");

        foreach (var item in line.Select(x => int.Parse(x)).Where(c => HeightList.Count == 0 || HeightList.Last() != c))
            HeightList.Add(item);

        int t = 0, t0 = 0;
        Heights = HeightList.ToDictionary(x => t++, x => x);
        Sorted = HeightList.OrderBy(i => i).ToDictionary(x => t0++, x => x);
        var lookup = Sorted.ToLookup(x => x.Value, x => x.Key);

        foreach (var item in Heights)
        {
            if (item.Key != 0)
            {
                var key = item.Key;
                var value = item.Value;
                var sortedKey = 0;
                var sortedKeys = lookup[value];

                for (int i = 0; i < sortedKeys.Count(); i++)
                {
                    if (Locked.ContainsKey(sortedKeys.ElementAt(i)) == false)
                    {
                        sortedKey = sortedKeys.ElementAt(i);
                        break;
                    }
                }
                if (sortedKey != 0)
                {
                    var sortedValue = Sorted[sortedKey];
                    var sortedBeforeValue = Sorted[sortedKey - 1];

                    if (item.Value == sortedValue && Heights[key - 1] == sortedBeforeValue)
                    {
                        Lefts.Add((sortedBeforeValue, sortedValue));
                        Locked.Add(sortedKey, sortedValue);
                    }
                }
                if (!Lefts.Remove((Heights[key - 1], value)))
                    Cut++;
            }
        }
        Console.WriteLine(Cut);
    }
}
