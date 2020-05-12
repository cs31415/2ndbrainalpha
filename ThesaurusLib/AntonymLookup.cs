using System.Collections.Generic;
using System.IO;

namespace ThesaurusLib
{
    public class AntonymLookup : IAntonymLookup
    {
        private readonly MultiMap<string, string> _antonyms;

        public AntonymLookup(string path)
        {
            var antonymDataSetFileName = $@"{path}\Thes_Antonyms_a-z.csv";
            _antonyms = new MultiMap<string, string>();
            bool headerRow = true;
            foreach (var line in File.ReadAllLines(antonymDataSetFileName))
            {
                if (headerRow)
                {
                    headerRow = false;
                    continue;
                }
                var parts = line.Split(',');
                var word = parts[0].Split('_')[0];
                for (int i = 1; i < parts.Length; i++)
                {
                    var antonym = parts[i];
                    if (!string.IsNullOrWhiteSpace(antonym) && !_antonyms.Contains(word, antonym))
                    {
                        _antonyms.Add(word, antonym);
                    }
                }
            }
        }

        public IList<string> GetAntonyms(string word)
        {
            return _antonyms[word];
        }
    }
}