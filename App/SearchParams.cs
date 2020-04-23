using System.Collections.Generic;

namespace _2ndbrainalpha
{
    public class SearchParams
    {
        public string Path {get; set; }
        public string Filter {get;set;}
        //public string SearchPattern {get;set;}
        public IList<string> TargetWords {get;set;}
    }
}
