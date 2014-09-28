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
            this.m_Clue = clue.ToLower();
            this.m_Pattern = pattern;
        }

        internal Word[] DoubleDefinitions()
        {
            IEnumerable<Word> result = Word.Matching(m_Pattern);
            foreach (var cluePart in m_Clue.Split(' ', '_'))
            {
                var candidates = new Word(cluePart).AllExceptAntonym;
                result = result.Where(candidates.Contains);
            }
            return result.Distinct().ToArray();
        }

        internal Word[] Included()
        {
            var clue = m_Clue.Replace(" ", "").Replace("?", "");
            IEnumerable<Word> result = Word.Matching(m_Pattern).Where(c => clue.Contains(c.Text));
            return result.ToArray();
        }

        internal Word[] StraightPlusIncluded()
        {
            var result = Included().Where(new Word(m_Clue.Split(' ', '_')[0]).AllExceptAntonym.Contains);
            return result.ToArray();
        }

        internal Word[] Anagram()
        {
            var clueParts = m_Clue.Split(' ', '_').Where(cp => cp.Length == m_Pattern.Replace(" ", "").Length);
            return clueParts.SelectMany(AnagramsOfWord).ToArray();
        }

        private Word[] AnagramsOfWord(string input)
        {
            var candidates = Word.Matching(new String('_', input.Length));
            return candidates.Where(w => w.Text != input && w.Text.OrderBy(c => c).SequenceEqual(input.OrderBy(c => c))).Distinct().ToArray();
        }

        internal Word[] StraightPlusAnagram()
        {
            var result = Anagram().Where(new Word(m_Clue.Split(' ', '_')[0]).AllExceptAntonym.Contains);
            return result.ToArray();
        }
    }
}
