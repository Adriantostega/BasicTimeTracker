using System;

namespace BasicTimeTracker.Model
{
    public interface ITrackingRecordEntity
    {
        int Id { get; }
        string ActivityName { get; }
        DateTime StartDateTime { get; }
        DateTime? EndDateTime { get;}
    }
}