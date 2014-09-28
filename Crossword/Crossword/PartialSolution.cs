using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword
{
    class PartialSolution : IEnumerable<Word>
    {
        private IEnumerable<Word> m_Candidates;
        private string[] m_Clue;

        public PartialSolution(IEnumerable<Word> candidates, string[] clue)
        {
            m_Candidates = candidates.Distinct().ToArray();
            m_Clue = clue;
        }

        IEnumerator IEnumerable.GetEnumerator() { return m_Candidates.GetEnumerator(); }
        public IEnumerator<Word> GetEnumerator() { return m_Candidates.GetEnumerator(); }

        internal Word[] PlusStraight()
        {
            var result = m_Candidates.Where(new Word(m_Clue[0]).AllExceptAntonym.Contains);
            return result.ToArray();
        }
    }
}
