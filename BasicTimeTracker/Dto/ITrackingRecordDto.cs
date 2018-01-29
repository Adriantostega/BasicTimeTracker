using System;

namespace BasicTimeTracker.Dto
{
    public interface ITrackingRecordDto
    {
        string ActivityName { get; set; }
        DateTime StartDateTime { get; set; }
        DateTime? EndDateTime { get; set; }      
    }
}