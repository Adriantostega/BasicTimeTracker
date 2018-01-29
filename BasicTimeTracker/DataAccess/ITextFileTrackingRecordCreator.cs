using System.Collections.Generic;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public interface ITextFileTrackingRecordCreator
    {
        List<ITrackingRecordEntity> CreateFromFile(string fileName);
    }
}