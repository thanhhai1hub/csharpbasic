using System;
using System.IO;
using System.Collections.Generic;
using Contact;
using CarlosDataAnalyser;
using System.Linq;
using System.Collections.Concurrent;

namespace CsharpBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = @"Data";

            List<IDataAnalyser> Analysers = new List<IDataAnalyser>();


            int[] hashArray = new int[499999900];

            string[] hashToStringArray = new string[10];

            string[] filePaths = Directory.GetFiles(path);


            foreach (string filePath in filePaths)
            {
                Analysers.Add(new CarlosAnalyser(filePath));
            }

            foreach (IDataAnalyser analyser in Analysers)
            {
                analyser.ProcessFile(analyser.Path, ref hashArray);
            }


            #region
            Array.Sort(hashArray);
            Dictionary<int, int> top10Hashes = new Dictionary<int, int>();
            int minFrequencyInTop5 = 0;

            int currentHash = hashArray[0];
            int currentCount = 1;

            for (int i = 1; i < hashArray.Length; i++)
            {
                if (hashArray[i] == currentHash)
                {
                    currentCount++;
                }
                else
                {
                    TryUpdateTop10(top10Hashes, ref minFrequencyInTop5, currentHash, currentCount);
                    currentHash = hashArray[i];
                    currentCount = 1;
                }
            }
            TryUpdateTop10(top10Hashes, ref minFrequencyInTop5, currentHash, currentCount);


            #endregion

            foreach (IDataAnalyser analyser in Analysers)
            {
                analyser.Mapping(analyser.Path, hashToStringArray, top10Hashes);
            }
            hashArray = null;
            Console.WriteLine("Top 10 chuỗi xuất hiện nhiều nhất:");

            foreach (var item in hashToStringArray)
            {
                Console.WriteLine($"Chuỗi: {item}");
            }
            foreach (var x in top10Hashes)
            {
                Console.WriteLine($"");
            }
            Console.ReadKey();

        }

        // Hàm để cập nhật dictionary
        static void TryUpdateTop10(Dictionary<int, int> top10Hashes, ref int minFrequencyInTop5, int hash, int count)
        {
            if (top10Hashes.Count < 10)
            {
                top10Hashes[hash] = count;
            }
            else
            {
                var minKey = top10Hashes.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

                if (count > top10Hashes[minKey])
                {
                    top10Hashes.Remove(minKey);
                    top10Hashes[hash] = count;
                }
            }

            minFrequencyInTop5 = top10Hashes.Count > 0 ? top10Hashes.Values.Min() : 0;
        }


    }

}

