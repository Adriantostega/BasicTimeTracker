using System;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public class TextTrackingRecordEntity : TrackingRecordEntity
    {     
        public string FileNameWithPath { get; set; }
    }
}