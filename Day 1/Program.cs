using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllLines(path).ToList();

//Part 1

var sw = Stopwatch.StartNew();
var left = new List<int>();
var right = new List<int>();

foreach (var line in input)
{
    left.Add(Convert.ToInt32(line.Split("   ").First().Trim()));
    right.Add(Convert.ToInt32(line.Split("   ").Last().Trim()));
}

left = left.Order().ToList();
right = right.Order().ToList();

int sum = 0;

for (int i = 0; i < left.Count; i++)
    sum = sum + Math.Abs(left[i] - right[i]);

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2

sw.Restart();

sum = 0;
for (int i = 0; i < left.Count; i++)
    sum += left[i] * (right.Count(x => x.Equals(left[i])));

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");
