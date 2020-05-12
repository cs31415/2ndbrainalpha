using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ThesaurusLib
{
    /// <summary>
    /// Helper class for looking up entries in the thesaurus
    /// </summary>
    public class Lookup
    {
        public static IList<Entry> GetSynonyms(string word)
        {
            int lineNumber = GetLineNumber(word);
            IList<Entry> synonyms = new List<Entry>();
            if (lineNumber > 0)
            {
                var synonymsFilePath = $"th_en_US_new.dat";
                var fs = new FileStream(synonymsFilePath, FileMode.Open, FileAccess.Read);
                using (var reader = new StreamReader(fs))
                {
                    fs.Seek(lineNumber, SeekOrigin.Begin);
                    var hdr = reader.ReadLine();
                    var nLines = Convert.ToInt32(hdr.Split('|')[1]);
                    for (int i = 0; i < nLines; i++)
                    {
                        var line = reader.ReadLine();
                        var words = line.Split('|');
                        var type = Regex.Replace(words[0], @"[()]", "");
                        synonyms = synonyms.Concat(words.Skip(1).Where(w => w.Trim() != word).Select(w => new Entry { Type = type.Trim(), Word = w.Trim() })).ToList();
                    }
                }
            }
            return synonyms;
        }

        private static int GetLineNumber(string word)
        {
            var cs = $"Data Source=synonyms.db;";
            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = $"SELECT Line_nb FROM tblSynonymsIndex WHERE Word_xt='{word}'";
            var response = cmd.ExecuteScalar();
            return Convert.ToInt32(response);
        }
    }
}
