namespace _2ndbrainalpha
{
    class TargetWord
    {
        public string Word { get; set; }
        public int MatchCount { get; set; }

        public TargetWord(string word, int matchCount)
        {
            Word = word;
            MatchCount = matchCount;
        }

        public override string ToString()
        {
            return $"{Word} ({MatchCount})";
        }
    }
}
