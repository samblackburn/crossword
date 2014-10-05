using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System;

namespace Crossword.Tests
{
    [TestFixture]
    class GuardianTests
    {
        [Test]
        public void InitialGuessingAlgo()
        {
            Assert.That(Cryptic29_09_2014((clue, pattern) => new Solver(clue, pattern).Guesses()), Is.GreaterThan(0.056));
        }

        [Test]
        public void GuessEverythingAlgo()
        {
            Assert.That(Cryptic29_09_2014((clue, pattern) => Word.Matching(pattern)), Is.GreaterThan(0.46));
        }

        [Test]
        public void AllSynonymsThenEverythingAlgo()
        {
            Assert.That(Cryptic29_09_2014((clue, pattern) => {
                var candidates = Word.Matching(pattern);
                return clue.Split().SelectMany(w => new Word(w).AllExceptAntonym).Where(candidates.Contains).Concat(candidates);
            }), Is.GreaterThan(0.52));
        }

        private double Cryptic29_09_2014(Func<string, string, IEnumerable<Word>> algo, bool verbose = false)
        {
            var testCases = new Dictionary<string, string> {
                {"ABBACY", "Superior office" },
                {"ADAMS RIB", "From the old gardener’s chest came material for his mate" },
                {"ANALGESIC", "Bill without a single wrong number" },
                {"BANANA", "Prohibition put on two articles of fruit" },
                {"BEAR CUB", "A grizzly infant?" },
                {"BEAST", "It’s most desirable to enclose a wild animal" },
                {"BECOME", "Change into suit" },
                {"BRER", "Uncle Remus’s brother" },
                {"CABIN", "Shanty for the rest of the sailors" },
                {"CONFIDENCE", "Private communication of faith" },
                {"CURTAIN-UP", "Start of playtime" },
                {"DOCTRINE", "The principle of no credit" },
                {"DORMANT", "Walked back around a chap sleeping" },
                {"FOOLSCAP", "Paper hat for the dunce?" },
                {"LIMB", "Dance circle lost member" },
                {"LOOSE ENDS", "They are free finally, but should still be tied up" },
                {"MACKINTOSH", "Cover for an art-nouveau designer" },
                {"MAIL", "Post filled by a knight?" },
                {"MALINGERER", "He stays well away from work" },
                {"OLD SCHOOL TIE", "Source of unfair advantage that could result in cold and hostile exchange around jobcentre" },
                {"PAGE", "Call sheet?" },
                {"PEBBLES", "Not precious stones, but possibly worn" },
                {"PERMIT", "Allow to advise on how to treat straight hair" },
                {"RAPID", "Fast, clear round for father in the ascendancy" },
                {"REALLOCATION", "Actual site for another share out" },
                {"REALM", "Actual number in the kingdom" },
                {"RESTRAIN", "The others have sound rule for control" },
                {"RETROGRADE", "Order great changes, going in a bad direction" },
                {"TAPERED", "Dispensed with red tape — came to the point" },
                {"TOADSTOOL", "May be spotted in the woods, wearing a cap"},
            };
            double score = 0;
            foreach (var testCase in testCases)
            {
                var pattern = new Regex("[A-Z]").Replace(testCase.Key, "_");
                var guesses = algo(testCase.Value, pattern).Select(w => w.Text.ToUpper()).ToArray();
                double thisScore = ScoreForGuessArray(testCase, guesses);
                score += thisScore;
                if (!verbose) continue;
                Console.WriteLine("Clue: {0} ", testCase.Value);
                Console.WriteLine("Answer: {0} ", testCase.Key);
                Console.WriteLine("Score: {0} ", thisScore);
                //Console.WriteLine(String.Join(Environment.NewLine, guesses));
            }
            return score / testCases.Count;
        }

        private static double ScoreForGuessArray(KeyValuePair<string, string> testCase, string[] guesses)
        {
            for (int i = 0; i < guesses.Length; i++) 
                if (guesses[i] == testCase.Key) 
                    return (testCase.Key.Length - Math.Log(i + 1) / Math.Log(26)) / testCase.Key.Length;
            return 0;
        }
    }
}
