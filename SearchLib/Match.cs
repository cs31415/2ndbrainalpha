namespace SearchLib
{
    public class Match
    {
        public string File { get; set; }
        public string Line { get; set; }
        public string Word { get; set; }
        public int Position { get; set; }
        public int LineNumber { get; set; }
        public int StartIndex { get; set; }
        public int LineStartIndex { get; set; }
        public int LineEndIndex { get; set; }

        public Match(
            string file, 
            string line, 
            string word, 
            int lineNumber, 
            int startIndex, 
            int position,
            int lineStartIndex,
            int lineEndIndex)
        {
            File = file;
            Line = line;
            Word = word;
            LineNumber = lineNumber;
            StartIndex = startIndex;
            Position = position;
            LineStartIndex = lineStartIndex;
            LineEndIndex = lineEndIndex;
        }
    }
}
