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
                layers[i] = new LayerData(layerData, i + 1, width, height);
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

        private class LayerData
        {
            public LayerData(string data, int layerNr, int width, int height)
            {
                LayerNr = layerNr;
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
            public int LayerNr { get; }
            public string origData { get; }
            public int[][] Data { get; }
        }
        
    }
}