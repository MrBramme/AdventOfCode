using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day08 : Solution
    {
        public override void Run(bool test = false)
        {
            var data = GetData(test);
            int width = 25;
            int height = 6;
            if (test)
            {
                width = 3;
                height = 2;
            }
            var layerdata = GetLayerData(data, width, height);
            Console.WriteLine($"Phase 1 Result: {SolvePhase1(layerdata)}");
            Console.WriteLine("Phase 2 result:");
            SolvePhase2(layerdata, width, height);
        }

        private string GetData(bool test)
        {
            string data;
            if (test)
            {
                data = "123456789012";
            }
            else
            {
                data = File.ReadAllText("Data/2019/08.txt");
            }
            return data;
        }

        private IEnumerable<LayerData> GetLayerData(string data, int width, int height)
        {
            if (data.Length % (width * height) != 0)
            {
                throw new ArgumentOutOfRangeException("data" ,"mismatch between data length & width*height");
            }

            var layerSize = width * height;
            var nrOfLayers = data.Length / layerSize;
            var layers = new LayerData[nrOfLayers];

            for(var i = 0; i < nrOfLayers; i++)
            {
                var layerData = data.Substring(i * layerSize, layerSize);
                layers[i] = new LayerData(layerData, width, height);
            }
            return layers;
        }

        private int SolvePhase1(IEnumerable<LayerData> layers)
        {
            var minZeroCount = int.MaxValue;
            LayerData l = layers.First();
            foreach (var layer in layers)
            {
                var zeroCount = layer.LayerChars.Count(x => x.Equals('0'));
                if (zeroCount < minZeroCount)
                {
                    minZeroCount = zeroCount;
                    l = layer;
                }
            }
            var oneCount = l.LayerChars.Count(x => x.Equals('1'));
            var twoCount = l.LayerChars.Count(x => x.Equals('2'));
            return oneCount * twoCount;
        }

        private void SolvePhase2(IEnumerable<LayerData> layers, int width, int height)
        {
            // 0 black, 1 white, 2 transp
            var result = new int[height][];
            for(var h = 0; h < height; h++)
            {
                result[h] = new int[width];
                for (var w = 0; w < width; w++)
                {
                    foreach (var layer in layers)
                    {
                        if (layer.Data[h][w] < 2)
                        {
                            result[h][w] = layer.Data[h][w];
                            break;
                        }
                    }
                }
            }

            foreach(var row in result)
            {
                foreach(var c in row)
                {
                    if (c == 0)
                    {
                        Console.Write(" ");
                    } else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }

        private class LayerData
        {
            public LayerData(string data, int width, int height)
            {
                origData = data;

                Data = new int[height][];
                var charArr = origData.ToCharArray();
                LayerChars = new char[charArr.Length];
                Array.Copy(charArr, LayerChars, charArr.Length);
                for (var i = 0; i < height; i++)
                {
                    Data[i] = LayerChars.Skip(i*width).Take(width).Select(x => int.Parse(x.ToString())).ToArray();
                }
                
            }

            public char[] LayerChars { get; }
            public string origData { get; }
            public int[][] Data { get; }
        }
        
    }
}