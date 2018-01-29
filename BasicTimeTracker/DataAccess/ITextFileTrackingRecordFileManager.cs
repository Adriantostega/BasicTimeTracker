using System.Collections.Generic;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public interface ITextFileTrackingRecordFileManager
    {
        void UpdateContentOfFile(string fileNameWithPath, List<ITrackingRecordEntity> trackingRecordEntitiesFromFile);
    }
}