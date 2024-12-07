using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllLines(path).ToList();

//Part 1

var sw = Stopwatch.StartNew();
Int64 sum = 0;

foreach (var line in input)
{
    Int64 goal;
    goal = Convert.ToInt64(line.Split(':')[0]);
    var values = line.Split(": ")[1].Split(" ").Select(x=>Convert.ToInt64(x)).ToList();
    var valueList = new List<Int64>();
    valueList.Add(values[0]);
    values.Remove(values[0]);
    while (values.Count > 0)
    {
        var tempList = new List<Int64>();
        tempList.AddRange(valueList.Select(x => x + values.First()));
        tempList.AddRange(valueList.Select(x => x * values.First()));
        valueList = tempList;
        values.Remove(values[0]);
    }
    if (valueList.Contains(goal))
        sum += goal;
}

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2

sw.Restart();
sum = 0;

foreach (var line in input)
{
    Int64 goal;
    goal = Convert.ToInt64(line.Split(':')[0]);
    var values = line.Split(": ")[1].Split(" ").Select(x => Convert.ToInt64(x)).ToList();
    var valueList = new List<Int64>();
    valueList.Add(values[0]);
    values.Remove(values[0]);
    while (values.Count > 0)
    {
        var tempList = new List<Int64>();
        tempList.AddRange(valueList.Select(x => Convert.ToInt64(x.ToString() + values.First().ToString())));
        tempList.AddRange(valueList.Select(x => x + values.First()));
        tempList.AddRange(valueList.Select(x => x * values.First()));
        valueList = tempList.Where(x => x <= goal).ToList();
        values.Remove(values[0]);
    }
    if (valueList.Contains(goal))
        sum += goal;
}

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");