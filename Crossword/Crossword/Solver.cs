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

        internal Word[] DoubleDefinitions()
        {
            IEnumerable<Word> result = Word.Matching(m_Pattern);
            foreach (var cluePart in m_Clue.Split(' ', '_'))
            {
                var candidates = new Word(cluePart).Similar.Concat(new Word(cluePart).Hyponym);
                result = result.Where(candidates.Contains);
            }
            return result.Distinct().ToArray();
        }
    }
}
