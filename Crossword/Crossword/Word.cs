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
        public override bool Equals(object other) { return other is Word && Text.Equals(((Word)other).Text); }
        public override int GetHashCode() { return Text.GetHashCode(); }
        public Word(string text) { Text = text; }

        public Word[] Antonym { get { return Join("wn_antonym"); } }
        public Word[] Attribute { get { return Join("wn_attr_adj_noun"); } }
        public Word[] Cause { get { return Join("wn_cause"); } }
        public Word[] Class { get { return Join("wn_class_member"); } }
        public Word[] Derived { get { return Join("wn_derived"); } }
        public Word[] Entails { get { return Join("wn_entails"); } }
        public Word[] Hypernym { get { return Join("wn_hypernym"); } }
        public Word[] Hyponym { get { return Join("wn_hyponym"); } }
        public Word[] Member { get { return Join("wn_mbr_meronym"); } }
        public Word[] Part { get { return Join("wn_part_meronym"); } }
        public Word[] Particple { get { return Join("wn_participle"); } }
        public Word[] Pertainym { get { return Join("wn_pertainym"); } }
        public Word[] See { get { return Join("wn_see_also"); } }
        public Word[] Similar { get { return Join("wn_similar"); } }
        public Word[] Subst { get { return Join("wn_subst_meronym"); } }
        public Word[] Group { get { return Join("wn_verb_group"); } }

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

        /// <param name="pattern">Underscore to mean any letter, space to mean a word separator, e.g. "t_ug_ c_ee_e"</param>
        internal static Word[] Matching(string pattern)
        {
            using (var conn = new MySqlConnection("Data Source=localhost;Database=wn_pro_mysql;User ID=sam;Password=;Old Guids=true;"))
            {
                conn.Open();
                var sql = String.Format("SELECT word FROM wn_synset WHERE word like '{0}'", pattern.Replace(" ", "\\_"));
                var result = new MySqlCommand(sql, conn).ExecuteReader();
                var list = new List<Word>();
                while (result.Read())
                {
                    list.Add(new Word(result["word"] as string));
                }
                return list.ToArray();
            }
        }
    }
}
