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
            var solutions = new Solver("northern sky", "____").DoubleDefinitions();
            Assert.AreEqual("blue", solutions.Single().Text);
        }

        [Test]
        public void TripleDefinition()
        {
            var solutions = new Solver("burning fuel sack", "____").DoubleDefinitions();
            Assert.AreEqual("fire", solutions.Single().Text);
        }

        [Test]
        public void Included()
        {
            var solutions = new Solver("Grass in-law? No!", "____").Included();
            Assert.AreEqual("lawn", solutions.Single().Text);
        }

        [Test]
        public void HarderIncluded()
        {
            // Need the straight here to eliminate "meet"
            var solutions = new Solver("book in to meeting", "____").StraightPlusIncluded();
            Assert.AreEqual("tome", solutions.Single().Text);
        }

        [Test]
        public void Anagram()
        {
            var solutions = new Solver("scramble on", "__").Anagram();
            Assert.AreEqual("no", solutions.Single().Text);
        }

        [Test]
        public void StraightPlusAnagram()
        {
            var solutions = new Solver("rock random notes", "_____").StraightPlusAnagram();
            Assert.AreEqual("stone", solutions.Single().Text);
        }
    }
}
