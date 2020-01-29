using Ganss.Text;
using SynonymsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2ndbrainalpha
{
    public class SearchHelper
    {
        Func<bool> CheckForCancellation;

        public Action<string> OnFile { get; }
        public Action<string, string, int, int> OnMatch { get; }
        public Action<string, Exception> OnException { get; }

        public SearchHelper(Func<bool> checkForCancellation, Action<string> onFile, Action<string, string, int, int> onMatch, Action<string, Exception> onException)
        {
            CheckForCancellation = checkForCancellation;
            OnFile = onFile;
            OnMatch = onMatch;
            OnException = onException;
        }

        public void ProcessFile(string file, AhoCorasick trie, IList<Entry> synonyms)
        {
            try
            {
                // find occurences of search word and synonyms in file
                var lines = File.ReadAllLines(file);
                bool writeFileHeader = true;
                var currentLineNumber = 0;
                foreach (var line in lines)
                {
                    currentLineNumber++;
                    if (CheckForCancellation())
                    {
                        return;
                    }

                    var matches = trie
                        .Search(line)
                        .Where(m => {
                            var chars = line.ToCharArray();
                            var leftSpace = IsWhiteSpace(chars[Math.Max(m.Index - 1, 0)]);
                            var rightSpace = IsWhiteSpace(chars[Math.Min(m.Index + m.Word.Length, line.Length - 1)]);
                            return leftSpace && rightSpace;
                        });

                    if (writeFileHeader && matches.Count() > 0) 
                    {
                        OnFile(file);
                        writeFileHeader = false;
                    }

                    matches
                        .ToList()
                        .ForEach(m => OnMatch(line, m.Word, currentLineNumber, m.Index));
                }
            }
            catch (Exception ex)
            {
                OnException(file, ex);
            }
        }

        private bool IsWhiteSpace(char c)
        {
            return string.IsNullOrWhiteSpace(c.ToString()) || Regex.IsMatch(c.ToString(), "[.,;\"{}]");
        }
    }
}
