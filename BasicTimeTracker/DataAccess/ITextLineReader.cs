using System.Collections.Generic;

namespace BasicTimeTracker.DataAccess
{
    public interface ITextLineReader
    {
        void SetText(string textContext);
        List<string> GetLines();
    }
}