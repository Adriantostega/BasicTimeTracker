using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.Dto
{
    class TrackingRecordWithEntityDto :ITrackingRecordDto
    {
        public ITrackingRecordEntity TrackingRecordEntity { get; }
        public string ActivityName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public void UpdateEntity()
        {
            var trackingRecordEntity = TrackingRecordEntity as TrackingRecordEntity;
            if (trackingRecordEntity != null)
            {
                trackingRecordEntity.ActivityName = ActivityName;
                trackingRecordEntity.StartDateTime = StartDateTime;
                trackingRecordEntity.EndDateTime = EndDateTime;
            }
        }

        public TrackingRecordWithEntityDto(ITrackingRecordEntity trackingRecordEntity)
        {
            TrackingRecordEntity = trackingRecordEntity;
            ActivityName = TrackingRecordEntity.ActivityName;
            StartDateTime = TrackingRecordEntity.StartDateTime;
            EndDateTime = TrackingRecordEntity.EndDateTime;
        }
    }
}
