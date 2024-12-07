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
var sum = 0;

//Part 1

var sw = Stopwatch.StartNew();
char[,] wordSearch = new char[input.First().Length, input.Count];

for (int i = 0; i < input.First().Length; i++)
    for (int j = 0; j < input.Count; j++)
        wordSearch[i, j] = input[j][i];

for (int i = 0; i < input.First().Length; i++)
    for (int j = 0; j < input.Count; j++)
        if (wordSearch[i, j] == 'X')
        {
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i - 1, j], wordSearch[i - 2, j], wordSearch[i - 3, j]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i + 1, j], wordSearch[i + 2, j], wordSearch[i + 3, j]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i, j - 1], wordSearch[i, j - 2], wordSearch[i, j - 3]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i, j + 1], wordSearch[i, j + 2], wordSearch[i, j + 3]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i - 1, j - 1], wordSearch[i - 2, j - 2], wordSearch[i - 3, j - 3]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i + 1, j + 1], wordSearch[i + 2, j + 2], wordSearch[i + 3, j + 3]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i + 1, j - 1], wordSearch[i + 2, j - 2], wordSearch[i + 3, j - 3]))
                    sum++;
            }
            catch { }
            try
            {
                if (CheckWord(wordSearch[i, j], wordSearch[i - 1, j + 1], wordSearch[i - 2, j + 2], wordSearch[i - 3, j + 3]))
                    sum++;
            }
            catch { }
        }

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {sum}");

//Part 2
sum = 0;
sw.Restart();

for (int i = 0; i < input.First().Length; i++)
    for (int j = 0; j < input.Count; j++)
        if (wordSearch[i, j] == 'A')
        {
            try
            {
                if (CheckX(wordSearch[i-1, j-1], wordSearch[i - 1, j+1], wordSearch[i +1, j-1], wordSearch[i +1, j+1]))
                    sum++;
            }
            catch { }
        }

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {sum}");

bool CheckWord(char letter1, char letter2, char letter3, char letter4)
{
    string word = $"{letter1}{letter2}{letter3}{letter4}";
    return word.Equals("XMAS");
}

bool CheckX(char tl, char tr, char bl, char br)
{
    return ((tl == 'M' && br == 'S') || (tl == 'S' && br == 'M')) && ((tr == 'M' && bl == 'S') || (tr == 'S' && bl == 'M'));
}