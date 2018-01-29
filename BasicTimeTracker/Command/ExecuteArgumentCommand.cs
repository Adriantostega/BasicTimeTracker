using System;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.Command
{
    public class ExecuteArgumentCommand : EventArgs
    {
        public ITrackingRecordDto TrackingRecordDto { get; private set; }

        public ExecuteArgumentCommand(ITrackingRecordDto trackingRecordDto)
        {
            TrackingRecordDto = trackingRecordDto;
        }
    }
}