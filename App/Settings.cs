using System.Collections.Generic;

namespace _2ndbrainalpha
{
    class Settings
    {
        public string Path { get; set; }
        public string SearchText { get; set; }
        public IList<string> TargetWords { get; set; }
        public string SelectedNode { get; set; }
    }
}
