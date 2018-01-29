using System;
using System.Collections.Generic;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public interface ITrackingRecordRepository
    {
        List<ITrackingRecordDto> ReadAll();
        ITrackingRecordDto Create(ITrackingRecordDto trackingRecordDto);
        void Update(ITrackingRecordDto trackingRecordDto);
        void Delete(ITrackingRecordDto trackingRecordDto);
    }
}