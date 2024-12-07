using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
var sw = Stopwatch.StartNew();
var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllLines(path).ToList();


//Part 1


var inputTiles = new char[input.First().Length, input.Count];

var startingX = 0;
var startingY = 0;

for (int y = 0; y < input.Count; y++)
    for (int x = 0; x < input[y].Length; x++)
    {
        switch (input[y][x])
        {
            case '#':
                inputTiles[x, y] = '#'; break;
            case '^':
                inputTiles[x, y] = '^';
                startingX = x;
                startingY = y;
                break;
            default:
                inputTiles[x, y] = '.'; break;
        }
    }

var tiles = inputTiles.Clone() as char[,];
var currentDir = 1;
var currentX = startingX;
var currentY = startingY;

try
{
    while (true)
    {
        char newTile;
        switch (currentDir)
        {
            case 1:
                newTile = tiles[currentX, currentY - 1];
                if (newTile != '#')
                {
                    currentY--;
                    tiles[currentX, currentY] = 'X';
                }
                else
                    currentDir = 2;
                break;
            case 3:
                newTile = tiles[currentX, currentY + 1];
                if (newTile != '#')
                {
                    currentY++;
                    tiles[currentX, currentY] = 'X';
                }
                else
                    currentDir = 4;
                break;
            case 4:
                newTile = tiles[currentX - 1, currentY];
                if (newTile != '#')
                {
                    currentX--;
                    tiles[currentX, currentY] = 'X';
                }
                else
                    currentDir = 1;
                break;
            case 2:
                newTile = tiles[currentX + 1, currentY];
                if (newTile != '#')
                {
                    currentX++;
                    tiles[currentX, currentY] = 'X';
                }
                else
                    currentDir = 3;
                break;
        }
    }
}
catch (Exception e)
{ }

Console.WriteLine($"Part 1 ({sw.ElapsedMilliseconds}ms): {VisitedCount(tiles)}");

//Part 2

sw.Restart();

var looped = 0;
for (int j = 0; j < input.Count; j++)
    for (int i = 0; i < input[j].Length; i++)
    {
        if (tiles[i, j] == 'X')
        {
            //Console.WriteLine($"Checking {i},{j}");
            HashSet<int> tileHash = new HashSet<int>();
            char[,] tiles2 = inputTiles.Clone() as char[,];
            tiles2[i, j] = '#';
            currentX = startingX;
            currentY = startingY;
            currentDir = 1;

            try
            {
                var stepCount = 0;
                tileHash.Add(currentX * 100000 + currentY * 100 + currentDir);
                do
                {
                    char newTile;
                    stepCount = tileHash.Count;
                    switch (currentDir)
                    {
                        case 1:
                            newTile = tiles2[currentX, currentY - 1];
                            if (newTile != '#')
                            {
                                currentY--;
                                tiles2[currentX, currentY] = 'X';
                            }
                            else
                                currentDir = 2;
                            break;
                        case 3:
                            newTile = tiles2[currentX, currentY + 1];
                            if (newTile != '#')
                            {
                                currentY++;
                                tiles2[currentX, currentY] = 'X';
                            }
                            else
                                currentDir = 4;
                            break;
                        case 4:
                            newTile = tiles2[currentX - 1, currentY];
                            if (newTile != '#')
                            {
                                currentX--;
                                tiles2[currentX, currentY] = 'X';
                            }
                            else
                                currentDir = 1;
                            break;
                        case 2:
                            newTile = tiles2[currentX + 1, currentY];
                            if (newTile != '#')
                            {
                                currentX++;
                                tiles2[currentX, currentY] = 'X';
                            }
                            else
                                currentDir = 3;
                            break;
                    }
                    tileHash.Add(currentX * 100000 + currentY * 100 + currentDir);
                }
                while (stepCount < tileHash.Count);
                looped++;
            }
            catch (Exception e)
            { }
        }
    }

Console.WriteLine($"Part 2 ({sw.ElapsedMilliseconds}ms): {looped}");

//All Tiles

sw.Restart();

looped = 0;
for (int j = 0; j < input.Count; j++)
    for (int i = 0; i < input[j].Length; i++)
    {
        //Console.WriteLine($"Checking {i},{j}");
        HashSet<int> tileHash = new HashSet<int>();
        char[,] tiles2 = inputTiles.Clone() as char[,];
        tiles2[i, j] = '#';
        currentX = startingX;
        currentY = startingY;
        currentDir = 1;

        try
        {
            var stepCount = 0;
            tileHash.Add(currentX * 100000 + currentY * 100 + currentDir);
            do
            {
                char newTile;
                stepCount = tileHash.Count;
                switch (currentDir)
                {
                    case 1:
                        newTile = tiles2[currentX, currentY - 1];
                        if (newTile != '#')
                        {
                            currentY--;
                            tiles2[currentX, currentY] = 'X';
                        }
                        else
                            currentDir = 2;
                        break;
                    case 3:
                        newTile = tiles2[currentX, currentY + 1];
                        if (newTile != '#')
                        {
                            currentY++;
                            tiles2[currentX, currentY] = 'X';
                        }
                        else
                            currentDir = 4;
                        break;
                    case 4:
                        newTile = tiles2[currentX - 1, currentY];
                        if (newTile != '#')
                        {
                            currentX--;
                            tiles2[currentX, currentY] = 'X';
                        }
                        else
                            currentDir = 1;
                        break;
                    case 2:
                        newTile = tiles2[currentX + 1, currentY];
                        if (newTile != '#')
                        {
                            currentX++;
                            tiles2[currentX, currentY] = 'X';
                        }
                        else
                            currentDir = 3;
                        break;
                }
                tileHash.Add(currentX * 100000 + currentY * 100 + currentDir);
            }
            while (stepCount < tileHash.Count);
            looped++;
        }
        catch (Exception e)
        { }
    }

Console.WriteLine($"All Tiles ({sw.ElapsedMilliseconds}ms): {looped}");

static Int32 VisitedCount(char[,] tiles)
{
    int count = 0;
    foreach (char tile in tiles)
        if (tile == 'X' || tile == '^')
            count++;
    return count;
}