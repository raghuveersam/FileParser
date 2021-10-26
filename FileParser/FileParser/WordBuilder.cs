using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileParser
{
    public class WordBuilder
    {

        #region Public Variables
        /// <summary>
        /// Largest word that can be formed with other words in the file
        /// </summary>
        public int FirstLargestWord
        {
            get
            {
                return this.firLargestWord;
            }
        }

        /// <summary>
        /// Second largest word that can be formed with other words in the file
        /// </summary>
        public int SecondLargestWord
        {
            get
            {
                return this.secLargestWord;
            }
        }

        /// <summary>
        /// Total number of words that can be formed with other words in the file
        /// </summary>
        public int MaximumCount
        {
            get
            {
                return this.maxCnt;
            }
        }
        #endregion

        #region Private Variables

        Dictionary<string, int> inputWords = new Dictionary<string, int>();
        Dictionary<string, int> resWords = new Dictionary<string, int>();
        private int firMinWordLen, secMinWordLen;
        private int firLargestWord, secLargestWord, maxCnt;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WordBuilder()
        {
            firMinWordLen = Int32.MaxValue; secMinWordLen = Int32.MaxValue;
            firLargestWord = Int32.MinValue; secLargestWord = Int32.MinValue;
            maxCnt = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generic method that computes the word list
        /// </summary>
        public void ComputeWords()
        {
            ReadFile(@"InputData.txt");
            DoWork();
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Function to read the file and load it to a dictionary. This dictionary will be used as a reference for computing the count
        /// </summary>
        /// <param name="location">File name that has to be parsed</param>
        private void ReadFile(string location)
        {
            string text = System.IO.File.ReadAllText(location);
            string[] lines = System.IO.File.ReadAllLines(location);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line.Trim()) && !inputWords.ContainsKey(line.Trim()))
                {
                    if (line.Length < firMinWordLen)
                    {
                        firMinWordLen = line.Length;
                    }
                    else if (line.Length > firMinWordLen && line.Length < secMinWordLen)
                    {
                        secMinWordLen = line.Length;
                    }

                    inputWords.Add(line, line.Length);
                }
            }
        }

        /// <summary>
        /// Compute the work of finding first largest word, second largest word
        /// </summary>
        private void DoWork()
        {
            int minRequiredLen = firMinWordLen + secMinWordLen;

            foreach (var item in inputWords.OrderBy(x => x.Value))
            {
                if (item.Value >= minRequiredLen && IsValidString(item.Key, inputWords, resWords))
                {
                    if (item.Value > firLargestWord)
                    {
                        secLargestWord = firLargestWord;
                        firLargestWord = item.Value;
                    }

                    resWords.Add(item.Key, item.Key.Length);
                }
            }

            maxCnt = resWords.Count;
        }

        /// <summary>
        ///  Find is the given string can be computed with any other string in the file
        /// </summary>
        /// <param name="curStr">Current string in the file</param>
        /// <param name="dic">Hashmap that contains all words in the file</param>
        /// <param name="resWord">List of words that can be computed with other words in the file</param>
        /// <returns></returns>
        private bool IsValidString(string curStr, Dictionary<string, int> dic, Dictionary<string, int> resWord)
        {

            dic.Remove(curStr);
            bool[] dp = new bool[curStr.Length + 1];
            dp[0] = true;

            for (int i = 0; i < curStr.Length + 1; i++)
            {
                for (int j = 0; j < i; ++j)
                {
                    // Validating on resWords will help is optimize the search for that computer earlier in the file
                    if (dp[j] && (dic.ContainsKey(curStr.Substring(j, i - j)) || resWord.ContainsKey(curStr.Substring(j, i - j))))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            dic.Add(curStr, curStr.Length);
            return dp[curStr.Length];
        }
        #endregion
    }
}
