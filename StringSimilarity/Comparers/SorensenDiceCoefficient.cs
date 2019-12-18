using System;
using StringSimilarity.Enums;
using StringSimilarity.Interfaces;

namespace StringSimilarity.Comparers
{
    public class SorensenDiceCoefficient : IStringComparer
    {
        public decimal CompareTwoStrings(string a, string b)
        {
            throw new NotImplementedException();
        }
        public bool AreStringsValid(params string[] p)
        {
            throw new NotImplementedException();
        }
        private decimal Compare()
        {
            return 0.0M;
        }
    }
}