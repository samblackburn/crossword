using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MySql.Data.MySqlClient;

namespace Crossword
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ConnectToSqlServer()
        {
            var conn = new MySqlConnection("Data Source=localhost;Database=test;User ID=sam;Password=;Old Guids=true;");
            conn.Open();
            var result = new MySqlCommand("SELECT 1", conn).ExecuteReader();
            Assert.True(result.Read());
            Assert.AreEqual(1, result[0]);
            Assert.False(result.Read());
        }
         
        [Test]
        public void CanConnectToDictionaryDatabase()
        {
            var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;");
            conn.Open();
            var result = new MySqlCommand("SELECT 1", conn).ExecuteReader();
            Assert.True(result.Read());
            Assert.AreEqual(1, result[0]);
            Assert.False(result.Read());
        }

        [Test]
        public void FoodlessMeansMalnourished()
        {
            var list = new Word("malnourished").Similar;
            CollectionAssert.Contains(list.Select(w => w.Text), "foodless");
        }
    }
}
