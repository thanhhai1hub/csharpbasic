using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact
{
    public interface IDataAnalyser
    {
        /// <summary>
        /// The author
        /// </summary>
        string Author { get; }
        /// <summary>
        /// The path of folder which stores data files
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Analyse data and 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>

        //IEnumerable<string> GetTopTenStrings(Dictionary<string, int> stringCounts);
        //void ReadFileToArray(string[] allStrings, ref int currentIndex);
        void ProcessFile(string filePath, ref int[] hashArray);
        void Mapping(string filePath, string[] hashToStringMap, Dictionary<int, int> top10Hashes);
    }
}
