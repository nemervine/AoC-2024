using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllLines(path).ToList();

//Part 1
var reportList = new List<List<int>>();
var dampedList = new List<List<int>>();

for (int i = 0; i < input.Count; i++)
    reportList.Add(input[i].Split(' ').Select(x => Convert.ToInt32(x)).ToList<int>());

var sw = Stopwatch.StartNew();

var sum = 0;
foreach (var report in reportList)
{
    if (isSafe(report))
        sum++;
    else
        dampedList.Add(report);
}

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2
//sw.Restart();

foreach (var report in dampedList)
{
    bool safe = true;
    for (int i = 0; i < report.Count; i++)
    {
        var dampedReport = new List<int>();
        dampedReport.AddRange(report);
        dampedReport.RemoveAt(i);
        safe = isSafe(dampedReport);
        if (safe)
            break;
    }
    if (safe)
    {
        sum++;
        //Console.WriteLine($"Report {dampedList.IndexOf(report)} Safe");
    }
    else
    {
        //Console.WriteLine($"Report {dampedList.IndexOf(report)} Unsafe");
    }
}

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");

bool isSafe(List<int> report)
{
    bool safe = true;
    bool decreasing = report[0] > report[1];
    for (int i = 0; i < report.Count - 1; i++)
    {
        switch (decreasing)
        {
            case true:
                if ((1 > (report[i] - report[i + 1])) || ((report[i] - report[i + 1]) > 3))
                    safe = false;
                break;
            case false:
                if ((1 > (report[i + 1] - report[i])) || ((report[i + 1] - report[i]) > 3))
                    safe = false;
                break;
        }
        if (!safe)
            break;
    }
    return safe;
}