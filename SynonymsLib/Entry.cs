namespace SynonymsLib
{
    public class Entry
    {
        public string Type { get; set; }
        public string Word { get; set; }
        public override string ToString()
        {
            return $"{Word} ({Type})";
        }
    }
}
