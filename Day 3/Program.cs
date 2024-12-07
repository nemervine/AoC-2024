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
var input = System.IO.File.ReadAllText(path);

//Part 1
const string pattern = @"(?:mul\()(\d{1,3})(?:,)(\d{1,3})(?:\))";
var sw = Stopwatch.StartNew();

var matches = Regex.Matches(input, pattern);

long sum = 0;

for (int i = 0; i < matches.Count; i++)
    sum += int.Parse(matches[i].Groups[1].ValueSpan) * int.Parse(matches[i].Groups[2].ValueSpan);

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2

const string pattern2 = @"do\(\)|(?:mul\()(\d{1,3})(?:,)(\d{1,3})(?:\))|don\'t\(\)";
sw.Restart();

var matches2 = Regex.Matches(input, pattern2);
var enabled = true;
sum = 0;
var count = 0;
foreach (var m in matches2.Select(m => m))
    switch (m.Value)
    {
        case "do()":
            enabled = true;
            break;
        case "don't()":
            enabled = false;
            break;
        default:
            if (enabled)
            {
                count++;
                sum += Mul(m);
            }
            break;
    }
Console.WriteLine($"{count}");
Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");

int Mul(Match x)
{
    var num1 = int.Parse(x.Groups[1].Value);
    var num2 = int.Parse(x.Groups[2].Value);
    return (num1 * num2);
}