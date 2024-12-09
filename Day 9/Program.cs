using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllText(path).Select(x => Int32.Parse(x.ToString())).ToList();

//Part 1

var sw = Stopwatch.StartNew();
var id = 0;
var fileblocks = new Dictionary<int, string>();
long sum = 0;

for (int i = 0; i < input.Count; i++)
{
    for (int j = 0; j < input[i]; j++)
    {
        if (i % 2 == 0)
            fileblocks.Add(fileblocks.Count, id.ToString());
        else
            fileblocks.Add(fileblocks.Count, ".");
    }
    if (i % 2 == 0)
        id++;
}

for (int i = 0; i < fileblocks.Count; i++)
    if (fileblocks.Where(x => x.Key >= i).All(x => x.Value == "."))
        break;
    else if (fileblocks[i] == ".")
    {
        var tempBlock = fileblocks.Where(x => x.Value != ".").Last();
        fileblocks[i] = new string(tempBlock.Value);
        fileblocks[tempBlock.Key] = ".";
    }


foreach (var block in fileblocks.Where(x => x.Value != "."))
    sum += block.Key * Convert.ToInt32(block.Value);

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2

sw.Restart();

id = 0;
var filesystem = "";
sum = 0;

for (int i = 0; i < input.Count; i++)
{
    if (i % 2 == 0)
    {
        filesystem += string.Concat(Enumerable.Repeat($"f{id}f", input[i]));
        id++;
    }
    else
        filesystem += string.Concat(Enumerable.Repeat(".", input[i]));
}
id--;

for (int i = id; i > 0; i--)
{
    //Console.WriteLine(filesystem);
    string pattern = $"(f{i}f)+";
    var match = Regex.Match(filesystem, pattern);
    var matches = Regex.Matches(filesystem, pattern.Replace("+",""));
    var length = matches.Count;
    var openSpot = Regex.Match(filesystem.Substring(0, match.Index), $"\\.{{{length}}}");
    if (openSpot.Success)
    {
        filesystem = filesystem.Remove(match.Index,match.Length);
        filesystem = filesystem.Insert(match.Index, string.Concat(Enumerable.Repeat(".", length)));
        filesystem = filesystem.Remove(openSpot.Index, openSpot.Length);
        filesystem = filesystem.Insert(openSpot.Index, match.Value);
    }
}

int index = 0;
while (filesystem.Length > 0)
{
    if (filesystem[0] != '.')
    {
        var match = Regex.Match(filesystem, $"^f\\d+f");
        sum += index * Convert.ToInt32(match.Value.Replace("f", ""));
        filesystem = filesystem.Remove(0, match.Length);
    }
    else
        filesystem = filesystem.Remove(0, 1);
    index++;
}

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");
