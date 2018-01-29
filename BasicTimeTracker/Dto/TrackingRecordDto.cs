using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.Dto
{
    public class TrackingRecordDto : ITrackingRecordDto
    {      
        public string ActivityName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }        
    }
}
