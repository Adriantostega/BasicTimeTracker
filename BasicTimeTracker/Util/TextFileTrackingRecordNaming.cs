using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimeTracker.Util
{
    public class TextFileTrackingRecordNaming
    {
        private static TextFileTrackingRecordNaming _textFileTrackingRecordNaming;
        private TextFileTrackingRecordNaming()
        { }

        public static TextFileTrackingRecordNaming Instance()
        {
            if (_textFileTrackingRecordNaming == null)
            {
                _textFileTrackingRecordNaming = new TextFileTrackingRecordNaming();
            }

            return _textFileTrackingRecordNaming;
        }

        public string GenerateNameFromDate(DateTime dateTime, string extension)
        {
            return $"{dateTime.Year}{dateTime.Day}{dateTime.Month}.{extension}";
        }

        public string GenerateNameFromDateAndtime(DateTime dateTime, string extension)
        {
            return $"{dateTime.Year}{dateTime.Day}{dateTime.Month}{dateTime.Hour}{dateTime.Minute}{dateTime.Second}{dateTime.Millisecond}.{extension}";
        }
    }
}
