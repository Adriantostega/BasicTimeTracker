using System.Collections.Generic;
using System.Windows.Documents;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public interface ITrackingRecordDataSource
    {
        List<ITrackingRecordEntity> GetAllRecords();
        void Update(ITrackingRecordEntity recordEntity);
        TrackingRecordEntity Create(TrackingRecordEntity recordEntity);
        void Delete(int id);
    }
}