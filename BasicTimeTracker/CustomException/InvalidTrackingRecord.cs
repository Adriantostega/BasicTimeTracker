using System;

namespace BasicTimeTracker.CustomException
{
    class InvalidTrackingRecord : Exception
    {
        public InvalidTrackingRecord(string message) : base(message)
        {
        }
    }
}
