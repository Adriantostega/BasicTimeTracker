using System;

namespace BasicTimeTracker.Model
{
    public class TrackingRecordEntity : ITrackingRecordEntity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}