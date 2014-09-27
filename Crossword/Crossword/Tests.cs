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
            var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;");
            conn.Open();
            var result = new MySqlCommand("SELECT two.Word FROM wn_synset one, wn_synset two, wn_similar relation WHERE relation.synset_id_1 = one.synset_id AND relation.synset_id_2 = two.synset_id AND one.Word = 'malnourished'", conn).ExecuteReader();
            var list = new List<string>();
            while (result.Read())
            {
                list.Add(result["Word"] as string);
            }
            CollectionAssert.Contains(list, "foodless");
        }
    }
}
