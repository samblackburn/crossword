using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
    [TestFixture]
    class CluesTests
    {
        [Test]
        public void DoubleDefinition()
        {
            var blue = new Word("blue");

            var clue = "northern sky";
            var pattern = "____";
            var solutions = new Solver(clue, pattern).DoubleDefinitions();
            CollectionAssert.Contains(solutions.Select(s => s.Text), "blue");
        }
    }
}
