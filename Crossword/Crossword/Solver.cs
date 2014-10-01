using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword
{
    class Solver
    {
        private string m_Clue;
        private string m_Pattern;

        public Solver(string clue, string pattern)
        {
            this.m_Clue = clue;
            this.m_Pattern = pattern;
        }

        internal Word[] LongList { get { return Word.Matching(m_Pattern); } }

        internal Word[] DoubleDefinitions()
        {
            IEnumerable<Word> result = LongList;
            foreach (var cluePart in ClueParts)
            {
                var candidates = new Word(cluePart).AllExceptAntonym;
                result = result.Where(candidates.Contains);
            }
            return result.Distinct().ToArray();
        }

        internal PartialSolution Included()
        {
            IEnumerable<Word> result = LongList.Where(c => ClueText.Contains(c.Text));
            return new PartialSolution(result, ClueParts);
        }

        internal PartialSolution Anagram()
        {
            var clueParts = ClueParts.Where(cp => cp.Length == m_Pattern.Replace(" ", "").Length);
            return new PartialSolution(clueParts.SelectMany(AnagramsOfWord), ClueParts);
        }

        private PartialSolution AnagramsOfWord(string input)
        {
            var candidates = Word.Matching(new String('_', input.Length));
            var result = candidates.Where(w => w.Text != input && w.Text.OrderBy(c => c).SequenceEqual(input.OrderBy(c => c)));
            return new PartialSolution(result, ClueParts);
        }

        private string[] ClueParts { get { return m_Clue.ToLower().Replace("?", "").Replace("-", " ").Split(' ', '_'); } }
        private string ClueText { get { return m_Clue.ToLower().Replace("?", "").Replace("-", " ").Replace(" ", ""); } }

        internal Word[] Guesses()
        {
            return Included().Concat(Anagram()).ToArray();
        }
    }
}
