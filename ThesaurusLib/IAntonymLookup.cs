using System.Collections.Generic;

namespace ThesaurusLib
{
    public interface IAntonymLookup
    {
        IList<string> GetAntonyms(string word);
    }
}