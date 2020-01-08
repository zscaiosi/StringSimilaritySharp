using System;
using Xunit;
using StringSimilarity;
using StringSimilarity.Enums;

namespace StringSimilarity.Tests
{
    public class MatcherTest
    {
        protected Matcher matcher {get;set;}
        [Fact]
        public void IsEqual()
        {
            matcher = new Matcher(MethodsEnum.SorensenDice, "Caio Saldanha", "Caio Saldanha");
            var c = matcher.GetCoefficient();

            Assert.True(1.0M == c);
        }
        [Fact]
        public void IsGteThreeFourths()
        {
            matcher = new Matcher(MethodsEnum.SorensenDice, "Caio Eduardo Zangirolami Saldanha", "Caio Zangirolami Saldanha");
            var c = matcher.GetCoefficient();

            Assert.True(c >= 0.75M);
        }
        [Fact]
        public void IsBestMatch()
        {
            matcher = new Matcher(
                MethodsEnum.SorensenDice,
                "Caio Eduardo Zangirolami Saldanha",
                "Caio Edurado Zangirolami Saldanha",
                "Caio Zangirolami Saldanha"
            );
            
            var r = matcher.GetBestMatch();

            Assert.Equal("Caio Edurado Zangirolami Saldanha", r);
        }
    }
}
