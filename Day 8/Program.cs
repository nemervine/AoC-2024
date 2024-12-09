using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

var debug = false;
//debug = true;
string path = debug ? "Test.txt" : "Input.txt";
var input = System.IO.File.ReadAllLines(path).ToList();

//Part 1

var points = new Dictionary<Point, char>();
var maxX = input[0].Length;
var maxY = input.Count;

for (int y = 0; y < maxY; y++)
    for (int x = 0; x < maxX; x++)
        if (input[y][x] != '.')
            points.Add(new Point(x, y), input[y][x]);

var antinodes = new Dictionary<char, List<Point>>();
var sw = Stopwatch.StartNew();

foreach (var antenna in points.Values.Distinct())
{
    var antennas = points.Where(x => x.Value == antenna).Select(x => x.Key).ToList();
    var nodeList = new List<Point>();
    foreach (var a in antennas)
    {
        var tempList = antennas.Where(x => x != a).ToList();
        foreach (var temp in tempList)
        {
            var tempNodes = GetAntinodes(a, temp,maxX,maxY);
            if (tempNodes.Count >= 2)
                nodeList.Add(tempNodes[1]);
        }
    }
    antinodes.Add(antenna, nodeList);
}

var nodes = new List<Point>();
foreach (var item in antinodes.Values)
{
    nodes.AddRange(item);
}

Console.WriteLine($"Part 1 ({sw.ElapsedTicks / 10000.0}ms): {nodes.Distinct().Count()}");

//Part 2

sw.Restart();
antinodes = new Dictionary<char, List<Point>>();

foreach (var antenna in points.Values.Distinct())
{
    var antennas = points.Where(x => x.Value == antenna).Select(x => x.Key).ToList();
    var nodeList = new List<Point>();
    foreach (var a in antennas)
    {
        var tempList = antennas.Where(x => x != a).ToList();
        foreach (var temp in tempList)
        {
            nodeList.AddRange(GetAntinodes(a, temp,maxX,maxY));
        }
    }
    antinodes.Add(antenna, nodeList);
}

nodes = new List<Point>();
foreach (var item in antinodes.Values)
{
    nodes.AddRange(item);
}

Console.WriteLine($"Part 2 ({sw.ElapsedTicks/10000.0}ms): {nodes.Distinct().Count()}");

Point GetAntinode(Point p1, Point p2)
{
    var antinode = new Point();
    if (p1.X < p2.X)
        antinode.X = p1.X - (p2.X - p1.X);
    else if (p1.X > p2.X)
        antinode.X = p1.X + (p1.X - p2.X);
    else
        antinode.X = p1.X;
    if (p1.Y < p2.Y)
        antinode.Y = p1.Y - (p2.Y - p1.Y);
    else if (p1.Y > p2.Y)
        antinode.Y = p1.Y + (p1.Y - p2.Y);
    else
        antinode.Y = p1.Y;
    return antinode;
}

List<Point> GetAntinodes(Point p1, Point p2, int maxX, int maxY)
{
    var nodeList = new List<Point>();
    nodeList.Add(p1);
    
    var currentNode = GetAntinode(p1,p2);
    while ((0 <= currentNode.X) && (0 <= currentNode.Y) && (currentNode.X < maxX) && (currentNode.Y < maxY))
    {
        nodeList.Add(new Point(currentNode.X, currentNode.Y));
        currentNode = GetAntinode(nodeList.Last(), nodeList[nodeList.Count-2]);
    }

    return nodeList;
}