using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringSimilarity.Enums;
using StringSimilarity.Interfaces;
using System.Linq;

namespace StringSimilarity.Comparers
{
    public class SorensenDiceCoefficient : IStringComparer
    {
        /// <summary>
        /// Compares two strings
        /// </summary>
        /// <param name="a">Base string</param>
        /// <param name="b">Comparing string</param>
        /// <returns>The similarity coefficient</returns>
        public decimal CompareTwoStrings(string a, string b)
        {
            if (!AreStringsValid(a, b))
                return 0.0M;
            
            a = Regex.Replace(a, @"\s+", "").ToLower();
            b = Regex.Replace(b, @"\s+", "").ToLower();
            // Builds up bigrams
            var firstBigrams = new Dictionary<string, int>();
            var secondBigrams = new Dictionary<string, int>();
            var intersectionSize = 0;

            for (var i = 0; i < a.Length - 1; i++)
            {
                var bigram = a.Substring(i, 2);
                var count = firstBigrams.ContainsKey(bigram) ?
                    firstBigrams[bigram] + 1
                :
                    1;
                // Updates value
                firstBigrams[bigram] = count;
            }

            for (var j = 0; j < b.Length - 1; j++)
            {
                var bigram = b.Substring(j, 2);
                var count = firstBigrams.ContainsKey(bigram) ?
                    firstBigrams[bigram]
                :
                    0;

                if (count > 0)
                {
                    firstBigrams[bigram] = count - 1;
                    intersectionSize += 1;
                }
            }

            return (decimal)Math.Round((2.0 * intersectionSize) / (a.Length + b.Length - 2), 2);
        }
        /// <summary>
        /// Checks if strings are valid
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool AreStringsValid(params string[] p)
        {
            var response = true;
            
            if ( p.Length % 2 != 0 )
                response = false;

            foreach (var _ in p)
            {
                if (string.IsNullOrEmpty(_))
                    response = false;
                if ( _.Length < 2 )
                    response = false;
            }

            return response;
        }
        /// <summary>
        /// Finds best match in a collection of strings
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public string FindBestMatch(string baseStr, params string[] inputs)
        {
            var coefficients = new Dictionary<decimal, string>();

            foreach (var i in inputs)
            {
                var c = CompareTwoStrings(baseStr, i);
                // Stores each string with its coefficient
                coefficients.Add(c, i);
            }

            var (key, val) = SortCoefficients(coefficients);

            return val;
        }
        /// <summary>
        /// Sorts the found coefficients
        /// </summary>
        /// <param name="Dictionary<decimal"></param>
        /// <param name="dic"></param>
        protected (decimal, string) SortCoefficients(Dictionary<decimal, string> dic)
        {
            decimal[] keys = new decimal[dic.Count];

            dic.Keys.CopyTo(keys, 0);

            Array.Sort(keys);

            return (keys[keys.Length - 1], dic[keys[keys.Length - 1]]);
        }
    }
}