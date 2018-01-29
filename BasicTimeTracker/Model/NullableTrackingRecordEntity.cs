using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimeTracker.Model
{
    public class NullableTrackingRecordEntity :ITrackingRecordEntity
    {
        private static NullableTrackingRecordEntity _nullableTrackingRecordEntity;
        public int Id { get; private set; }
        public string ActivityName { get; private set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime? EndDateTime { get; private set; }

        private NullableTrackingRecordEntity()
        {
            Id = 0;
            ActivityName = "";
            StartDateTime = DateTime.MinValue;
        }

        public static NullableTrackingRecordEntity Instance()
        {
            if (_nullableTrackingRecordEntity == null)
            {
                _nullableTrackingRecordEntity = new NullableTrackingRecordEntity();
                
            }
            return _nullableTrackingRecordEntity;
        }
    }
}
