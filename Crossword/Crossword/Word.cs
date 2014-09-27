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

        public Word(string text)
        {
            Text = text;
        }

        public string[] Similar
        {
            get
            {
                using (var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;"))
                {
                    conn.Open();
                    var result = new MySqlCommand("SELECT two.Word FROM wn_synset one, wn_synset two, wn_similar relation WHERE relation.synset_id_1 = one.synset_id AND relation.synset_id_2 = two.synset_id AND one.Word = '" + Text + "'", conn).ExecuteReader();
                    var list = new List<string>();
                    while (result.Read())
                    {
                        list.Add(result["Word"] as string);
                    }
                    return list.ToArray();
                }
            }
        }
    }
}
