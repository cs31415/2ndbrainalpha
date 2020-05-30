namespace SearchLib
{
    public class Match
    {
        public string File { get; set; }
        public string Line { get; set; }
        public string Word { get; set; }
        // starts from 1
        public int Position { get; set; }
        // starts from 1
        public int LineNumber { get; set; }
        // starts from 1
        public int StartIndex { get; set; }
        // starts from 1
        public int LineStartIndex { get; set; }
        // starts from 1
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
