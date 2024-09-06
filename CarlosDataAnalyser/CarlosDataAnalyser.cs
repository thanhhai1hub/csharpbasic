using Contact;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarlosDataAnalyser
{
    public class CarlosAnalyser : IDataAnalyser
    {
        public string Author => "carlos";

        public string Path
        {
            get;
            private set;
        }

        public CarlosAnalyser(string path)
        {
            this.Path = path;
        }

        public void ProcessFile(string filePath, ref int[] hashArray)
        {
            var index = 0;
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split(';');
                        for (int i = 0; i < strings.Length; i++)
                        {
                            strings[i] = strings[i].Trim();
                        }

                        foreach (var str in strings)
                        {
                            if (string.IsNullOrWhiteSpace(str))
                            {
                                continue;
                            }
                            int hash = str.GetHashCode();
                            hashArray[index] = hash;
                            index++;
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"I/O error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
        }


        public void Mapping(string filePath, string[] hashToStringArray, Dictionary<int, int> top10Hashes)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split(';');
                        int index = 0;
                        for (int i = 0; i < strings.Length; i++)
                        {
                            strings[i] = strings[i].Trim();
                        }
                        foreach (KeyValuePair<int, int> hash in top10Hashes)
                        {
                            foreach (var str in strings)
                            {
                                if (string.IsNullOrWhiteSpace(str))
                                {
                                    continue;
                                }
                                if (hash.Key == str.GetHashCode())
                                {
                                    hashToStringArray[index] = str;
                                    index++;
                                }

                            }
                        }

                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"I/O error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
        }

    }
}

