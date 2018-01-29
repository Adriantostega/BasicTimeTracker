using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimeTracker.Dto
{
    public class NullableTrackingRecordDto : ITrackingRecordDto
    {
        private static NullableTrackingRecordDto _nullableTrackingRecordDto;
        public string ActivityName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        private NullableTrackingRecordDto()
        {
            ActivityName = "";
            StartDateTime = DateTime.MinValue;
        }

        public static NullableTrackingRecordDto Instance()
        {
            if (_nullableTrackingRecordDto == null)
            {
                _nullableTrackingRecordDto = new NullableTrackingRecordDto();                
               
            }
            return _nullableTrackingRecordDto;
        }

      
    }
}
