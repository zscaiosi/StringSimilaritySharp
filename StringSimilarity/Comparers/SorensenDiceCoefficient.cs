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
        /// Rips off white spaces
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string Clear(string value)
        {
            var build = new System.Text.StringBuilder();

            foreach (var item in value)
            {
                if (item != ' ')
                {
                    build.Append(char.ToLower(item));
                }
            }

            return build.ToString();
        }

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

            a = Clear(a);
            b = Clear(b);

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
            if (p.Length % 2 != 0)
                return false;

            foreach (var _ in p)
            {
                if (string.IsNullOrEmpty(_))
                    return false;
                if (_.Length < 2)
                    return false;
            }

            return true;
        }
        /// <summary>
        /// Finds best match in a collection of strings
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public string FindBestMatch(string baseStr, params string[] inputs)
        {
            decimal currentCoefficient = 0;
            string value = null;

            foreach (var i in inputs)
            {
                var c = CompareTwoStrings(baseStr, i);

                if (c > currentCoefficient)
                {
                    currentCoefficient = c;
                    value = i;
                }

            }

            return value;
        }
    }
}