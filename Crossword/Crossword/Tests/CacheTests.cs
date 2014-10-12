using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Tests
{
    [TestFixture]
    class CacheTests
    {
        [Test]
        public void ReadInWholeDatabase()
        {
            CollectionAssert.Contains(WordCache.Instance["table"], "desk");
        }
    }
}
