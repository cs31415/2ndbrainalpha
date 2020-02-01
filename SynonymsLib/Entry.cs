namespace SynonymsLib
{
    /// <summary>
    /// Represents a synonym entry in the thesaurus
    /// </summary>
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
