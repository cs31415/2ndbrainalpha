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
        readonly Func<bool> _checkForCancellation;

        public Action<string> OnFile { get; }
        public Action<string, int> OnFileMatch { get; }
        public Action<Match, int> OnMatch { get; }
        public Action<string, Exception> OnException { get; }
        public Action OnComplete { get; }

        public SearchHelper(
            Func<bool> checkForCancellation, 
            Action<string> onFile,
            Action<string, int> onFileMatch, 
            Action<Match, int> onMatch, 
            Action<string, Exception> onException,
            Action onComplete)
        {
            _checkForCancellation = checkForCancellation;
            OnFile = onFile;
            OnFileMatch = onFileMatch;
            OnMatch = onMatch;
            OnException = onException;
            OnComplete = onComplete;
        }

        public void SearchFiles(string[] files, IList<string> words)
        {
            var trie = new AhoCorasick(words);
            if (files != null && files.Length > 0)
            {
                var tasks = new List<Task>();
                foreach (var file in files)
                {
                    OnFile(file);
                    Action search = async () => await SearchFile(file, trie);
                    tasks.Add(Task.Run(search));
                }

                Task.WaitAll(tasks.ToArray());
                OnComplete();
            }
        }

        /// <summary>
        /// Search file for list of words
        /// </summary>
        /// <param name="file"></param>
        /// <param name="trie"></param>
        public async Task SearchFile(string file, AhoCorasick trie)
        {
            try
            {
                // find occurrences of search word and synonyms in file
                using (var fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fs))
                {
                    var text = await reader.ReadToEndAsync();
                    var lines = text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                    var currentLineNumber = 0;
                    var nMatches = 0;
                    var matches = new List<Match>();
                    foreach (var line in lines)
                    {
                        if (_checkForCancellation())
                        {
                            return;
                        }

                        matches.AddRange(                            
                            trie
                                .Search(line)
                                .Where(m =>
                                {
                                    var chars = line.ToCharArray();
                                    var leftSpace = IsWhiteSpace(chars[Math.Max(m.Index - 1, 0)]);
                                    var rightSpace =
                                        IsWhiteSpace(chars[Math.Min(m.Index + m.Word.Length, line.Length - 1)]);
                                    return leftSpace && rightSpace;
                                })
                                .ToList()
                                .Select(m => new Match(file, line, m.Word, currentLineNumber, m.Index))
                            );

                        currentLineNumber++;
                    }
                    if (matches.Any())
                    {
                        var count = matches.Count();
                        OnFileMatch(file, count);
                    }

                    foreach (var m in matches)
                    {
                        OnMatch(m, ++nMatches);
                    }
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
