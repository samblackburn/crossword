using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Crossword
{
    class Word
    {
        public readonly string Text;
        public override string ToString() { return Text; }

        public Word(string text) { Text = text; }

        public Word[] Antonym { get { return Join("wn_antonym"); } }
        public Word[] Similar { get { return Join("wn_similar"); } }
        
        private Word[] Join(string table)
        {
            using (var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;"))
            {
                conn.Open();
                var sql = String.Format("SELECT two.Word FROM wn_synset one, wn_synset two, {0} relation WHERE relation.synset_id_1 = one.synset_id AND relation.synset_id_2 = two.synset_id AND one.Word = '{1}'", table, Text);
                var result = new MySqlCommand(sql, conn).ExecuteReader();
                var list = new List<Word>();
                while (result.Read())
                {
                    list.Add(new Word(result["Word"] as string));
                }
                return list.ToArray();
            }
        }
    }
}
