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
            var all = new ConcurrentDictionary<string, HashSet<string>>();
            using (var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;"))
            {
                conn.Open();
                Join(all, conn, "wn_antonym");
                Join(all, conn, "wn_attr_adj_noun");
                Join(all, conn, "wn_cause");
                Join(all, conn, "wn_class_member");
                Join(all, conn, "wn_derived");
                Join(all, conn, "wn_entails");
                Join(all, conn, "wn_hypernym");
                Join(all, conn, "wn_hyponym");
                Join(all, conn, "wn_mbr_meronym");
                Join(all, conn, "wn_part_meronym");
                Join(all, conn, "wn_participle");
                Join(all, conn, "wn_pertainym");
                Join(all, conn, "wn_see_also");
                Join(all, conn, "wn_similar");
                Join(all, conn, "wn_subst_meronym");
                Join(all, conn, "wn_verb_group");
            }
            CollectionAssert.Contains(all["table"], "desk");
        }

        private static void Join<T>(ConcurrentDictionary<string, T> all, MySqlConnection conn, string table) where T : ICollection<string>, new()
        {
            var sql = String.Format("SELECT one.Word, two.Word FROM wn_synset one, wn_synset two, {0} relation WHERE relation.synset_id_1 = one.synset_id AND relation.synset_id_2 = two.synset_id", table);
            using (var reader = new MySqlCommand(sql, conn).ExecuteReader())
            while (reader.Read())
            {
                var key = (string)reader[0];
                var val = (string)reader[1];
                all.AddOrUpdate(key, _ => new T { val }, (_, list) => { list.Add(val); return list; });
            }
        }
    }
}
