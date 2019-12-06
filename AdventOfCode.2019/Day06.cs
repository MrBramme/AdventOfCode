using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day06 : Solution
    {
        public override void Run(bool test = false)
        {
            var input = GetInput(test, 1);
            Console.WriteLine($"Result phase 1 : {ProcessInput(input)}");
            input = GetInput(test, 2);
            Console.WriteLine($"Result phase 2 : {FindSanta(input)}");
        }

        private string[] GetInput(bool test, int phase)
        {
            string data;
            if (test)
            {
                if (phase==1)
                {
                    data = $"B)C{Environment.NewLine}COM)B{Environment.NewLine}D)E{Environment.NewLine}C)D{Environment.NewLine}E)F{Environment.NewLine}B)G{Environment.NewLine}G)H{Environment.NewLine}D)I{Environment.NewLine}E)J{Environment.NewLine}J)K{Environment.NewLine}K)L"; // expected 42
                } else
                {
                    data = $"COM)B{Environment.NewLine}B)C{Environment.NewLine}C)D{Environment.NewLine}D)E{Environment.NewLine}E)F{ Environment.NewLine}B)G{ Environment.NewLine}G)H{ Environment.NewLine}D)I{ Environment.NewLine}E)J{ Environment.NewLine}J)K{ Environment.NewLine}K)L{ Environment.NewLine}K)YOU{Environment.NewLine}I)SAN";
                }
            } else
            {
                data = File.ReadAllText("Data/2019/06.txt");
            }
            return data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        private int ProcessInput(string[] input)
        {
            var data = input.Select(x => x.Split(')')).ToArray();
            var nodes = CreateTree(data);
            var result = 0;
            foreach(var node in nodes)
            {
                result += node.Value.Depth;
            }
            return result;
        }

        private int FindSanta(string[] input)
        {
            var data = input.Select(x => x.Split(')')).ToArray();
            var nodes = CreateTree(data);

            var currentNode = nodes["YOU"];
            var route = new Dictionary<string, TreeNode>();
            route.Add(currentNode.Value, new TreeNode(currentNode.Value, 0));
            for (var i = currentNode.Depth; i > 0; i--)
            {
                var newNode = route[currentNode.Value].AddChild(currentNode.Parent.Value);
                route.Add(newNode.Value, newNode);
                currentNode = currentNode.Parent;
            }

            var santaNode = nodes["SAN"];
            var santaRoute = new Dictionary<string, TreeNode>();
            santaRoute.Add(santaNode.Value, new TreeNode(santaNode.Value, 0));
            var result = -1;
            while (result < 0)
            {
                var newNode = santaRoute[santaNode.Value].AddChild(santaNode.Parent.Value);
                if (route.ContainsKey(newNode.Value))
                {
                    result = newNode.Depth + route[newNode.Value].Depth;
                } else
                {
                    santaRoute.Add(newNode.Value, newNode);
                    santaNode = santaNode.Parent;
                }
            }

            return result - 2;
        }
        private Dictionary<string, TreeNode> CreateTree(string[][] data)
        {
            var startPos = data.First(x => x[0].Equals("COM"));
            var nodes = new Dictionary<string, TreeNode>();
            nodes.Add(startPos[0], new TreeNode(startPos[0], 0));
            var toDo = data;
            while (toDo.Length > 0)
            {
                var toRetry = new List<string[]>();
                foreach (var x in toDo)
                {
                    if (nodes.ContainsKey(x[0]))
                    {
                        var node = nodes[x[0]].AddChild(x[1]);
                        nodes.Add(node.Value, node);
                    }
                    else
                    {
                        toRetry.Add(x);
                    }

                }
                toDo = toRetry.ToArray();
            }
            return nodes;
        }
        #region domain
        private class TreeNode
        {
            public TreeNode (string value, int depth, TreeNode parent = null)
            {
                Value = value;
                Depth = depth;
                Children = new LinkedList<TreeNode>();
                if (parent != null)
                {
                    Parent = parent;
                }
            }
            public string Value { get; }
            public TreeNode Parent { get; }
            public int Depth { get;  }
            public LinkedList<TreeNode> Children { get; set; }

            public TreeNode AddChild(string data)
            {
                var child = new TreeNode(data, Depth + 1, this);
                Children.AddFirst(child);
                return child;
            }
        }
        #endregion domain
    }
}