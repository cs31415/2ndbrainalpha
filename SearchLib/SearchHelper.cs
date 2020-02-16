using Ganss.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchLib
{
    /// <summary>
    /// Searches a file for occurences of a list of words
    /// </summary>
    public class SearchHelper
    {
        Func<bool> CheckForCancellation;

        public Action<string> OnFile { get; }
        public Action<string> OnFileMatch { get; }
        public Action<Match> OnMatch { get; }
        public Action<string, Exception> OnException { get; }

        public SearchHelper(
            Func<bool> checkForCancellation, 
            Action<string> onFile,
            Action<string> onFileMatch, 
            Action<Match> onMatch, 
            Action<string, Exception> onException)
        {
            CheckForCancellation = checkForCancellation;
            OnFile = onFile;
            OnFileMatch = onFileMatch;
            OnMatch = onMatch;
            OnException = onException;
        }

        public void SearchFiles(string[] files, IList<string> words)
        {
            var trie = new AhoCorasick(words);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    OnFile(file);
                    Task.Run(() => SearchFile(file, trie));
                }
            }
        }

        /// <summary>
        /// Search file for list of words
        /// </summary>
        /// <param name="file"></param>
        /// <param name="trie"></param>
        /// <param name="synonyms"></param>
        public void SearchFile(string file, AhoCorasick trie)
        {
            try
            {
                // find occurences of search word and synonyms in file
                var lines = File.ReadAllLines(file);
                bool writeFileHeader = true;
                var currentLineNumber = 0;
                foreach (var line in lines)
                {
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
                        OnFileMatch(file);
                        writeFileHeader = false;
                    }

                    matches
                        .ToList()
                        .ForEach(m => OnMatch(new Match(file, line, m.Word, currentLineNumber, m.Index)));

                    currentLineNumber++;
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
