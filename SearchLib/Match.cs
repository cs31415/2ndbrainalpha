namespace SearchLib
{
    public class Match
    {
        public string File { get; set; }
        
        public string Line { get; set; }
        
        public string Word { get; set; }

        // character position of match word within file; starts from 1
        public int Position { get; set; }
        
        // starts from 1
        public int LineNumber { get; set; }

        // index of match word within the line; starts from 1
        public int StartIndex { get; set; }
        
        // index of match line start within the file; starts from 1
        public int LineStartIndex { get; set; }

        // index of match line end within the file; starts from 1
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
