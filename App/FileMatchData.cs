using System.Collections.Generic;
using SearchLib;

namespace _2ndbrainalpha
{
    public class FileMatchData
    {
        public int MatchCount { get; set; }
        public IList<Match> Matches { get; set; }

        public FileMatchData(int matchCount)
        {
            MatchCount = matchCount;
            Matches = new List<Match>();
        }
    }
}
