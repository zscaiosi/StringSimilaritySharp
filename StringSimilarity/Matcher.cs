using System;
using StringSimilarity.Comparers;
using StringSimilarity.Enums;
using System.Linq;

namespace StringSimilarity
{
    public class Matcher
    {
        private string[] _words;
        private MethodsEnum _method;
        private SorensenDiceCoefficient _sdc;
        public Matcher(MethodsEnum method, params string[] words)
        {
            _method = method;
            _words = words;
        }
        /// <summary>
        /// Gets the similarity's coefficient between two strings depending on the chosen method
        /// </summary>
        /// <returns></returns>
        public decimal GetCoefficient()
        {
            switch (_method)
            {
                case MethodsEnum.SorensenDice:
                    var sd = new SorensenDiceCoefficient();
                    return sd.CompareTwoStrings(_words[0], _words[1]);
                case MethodsEnum.Levenshtein:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }

        public string GetBestMatch()
        {
            switch (_method)
            {
                case MethodsEnum.SorensenDice:
                    var sd = new SorensenDiceCoefficient();
                    return sd.FindBestMatch(_words[0], _words.Skip(1).ToArray());
                case MethodsEnum.Levenshtein:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
