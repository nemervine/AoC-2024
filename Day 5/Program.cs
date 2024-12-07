using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllText(path);
int sum = 0;
//Part 1

var sw = Stopwatch.StartNew();
var rules = input.Split(Environment.NewLine + Environment.NewLine)[0].Split(Environment.NewLine).ToList();
var updates = input.Split(Environment.NewLine + Environment.NewLine)[1].Split(Environment.NewLine).ToList();
var incorrect = new List<List<string>>();

for (int i = 0; i < updates.Count; i++)
{
    var pages = updates[i].Split(",").ToList();
    bool ordered = CheckRules(rules, pages);
    if (ordered)
        sum += Convert.ToInt32(pages[pages.Count / 2]);
    else
        incorrect.Add(pages);
}

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2

sw.Restart();
sum = 0;
List<int> swapCount = new List<int>();

for (int i = 0; i < incorrect.Count; i++)
{
    var pages = incorrect[i];
    var swaps = 0;
    while (!CheckRules(rules, pages))
    {
        for (int j = 0; j < rules.Count; j++)
        {
            var left = rules[j].Split("|")[0];
            var right = rules[j].Split("|")[1];
            if (pages.Contains(left) && pages.Contains(right))
                if (!(pages.IndexOf(left) < pages.IndexOf(right)))
                {
                    pages.Remove(right);
                    pages.Add(right);
                    swaps++;
                }
        }
    }
    swapCount.Add(swaps);
    sum += Convert.ToInt32(pages[pages.Count / 2]);
}
Console.WriteLine($"{swapCount.Min()}, {swapCount.Average()}, {swapCount.Max()}");
Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");

bool CheckRules(List<string> rules, List<string> pages)
{
    foreach (var rule in rules)
    {
        var left = rule.Split("|")[0];
        var right = rule.Split("|")[1];
        if (pages.Contains(left) && pages.Contains(right))
            if (!(pages.IndexOf(left) < pages.IndexOf(right)))
                return false;
    }
    return true;
}