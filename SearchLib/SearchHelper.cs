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
            var trie = new AhoCorasick(CharComparer.OrdinalIgnoreCase, words);
            if (files != null && files.Length > 0)
            {
                var tasks = new List<Task>();
                foreach (var file in files)
                {
                    if (_checkForCancellation())
                    {
                        return;
                    }

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
                    // TODO: StringSplitOptions.RemoveEmptyEntries is creating a problem with incorrect line numbers for matches!
                    var lines = text.Split(new[] {"\r\n", "\n", "\r"}, StringSplitOptions.None);
                    var currentLineNumber = 0;
                    var nMatches = 0;
                    var matches = new List<Match>();
                    int position = 0;
                    int endOfLinePosition = 0;
                    foreach (var line in lines)
                    {
                        if (_checkForCancellation())
                        {
                            return;
                        }

                        var carriageReturn = line.Length > 0 && line.ToCharArray()[line.Length - 1] == '\r';
                        endOfLinePosition = position + line.Length + (carriageReturn ? 0 : 1);

                        matches.AddRange(                            
                            trie
                                .Search(line)
                                .Where(m =>
                                {
                                    var chars = line.ToCharArray();
                                    var leftSpace = m.Index == 0 || IsWhiteSpace(chars[Math.Max(m.Index - 1, 0)]);
                                    var rightSpace =
                                        IsWhiteSpace(chars[Math.Min(m.Index + m.Word.Length, line.Length - 1)]);
                                    return leftSpace && rightSpace;
                                })
                                .ToList()
                                .Select(m => new Match(file, line, m.Word, 1+currentLineNumber, m.Index + 1, position + m.Index + 1, position + 1, endOfLinePosition + 1))
                            );

                        position = endOfLinePosition;
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
            return string.IsNullOrWhiteSpace(c.ToString()) || Regex.IsMatch(c.ToString(), "[.,;\"'{}()-:]");
        }
    }
}
