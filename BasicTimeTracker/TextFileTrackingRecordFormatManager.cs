using System;
using System.Globalization;
using BasicTimeTracker.DataAccess;
using BasicTimeTracker.Model;

namespace BasicTimeTracker
{
    internal class TextFileTrackingRecordFormatManager : ITextFileTrackingRecordFormatManager
    {
        public ITextTrackingRecordFormatValidator TextTrackingRecordFormatValidator { get; private set; }
        public TextFileTrackingRecordFormatManager(ITextTrackingRecordFormatValidator textTrackingRecordFormatValidator)
        {
            TextTrackingRecordFormatValidator = textTrackingRecordFormatValidator;
        }
        public bool IsFormatCorrect(string text)
        {
            var valuesFromText = text.Split('|');

            if (valuesFromText.Length == 4)
            {
                var idValueFromText = valuesFromText[0];
                var nameValueFromText = valuesFromText[1];
                var dateStartValueFromText = valuesFromText[2];
                var dateEndValueFromText = valuesFromText[3];

                int idParsed = -1;
                int.TryParse(idValueFromText,out idParsed);

                if (idParsed != -1)
                {
                    DateTime startDateTimeParsed;
                    DateTime.TryParse(dateStartValueFromText, out startDateTimeParsed);

                    if (startDateTimeParsed != DateTime.MinValue)
                    {
                        //DateTime endDateTimeParsed;
                        //DateTime.TryParse(dateStartValueFromText, out endDateTimeParsed);

                        return true;
                    }
                }
            }

            return false;
        }

        public TextTrackingRecordEntity GetTrackingRecordFromText(string text)
        {
            if (IsFormatCorrect(text))
            {
                var valuesFromText = text.Split('|');
                if (valuesFromText.Length == 4)
                {
                    var idValueFromText = valuesFromText[0];
                    var nameValueFromText = valuesFromText[1];
                    var dateStartValueFromText = valuesFromText[2];
                    var dateEndValueFromText = valuesFromText[3];                    

                    var trackingRecordEntity = new TextTrackingRecordEntity();
                    trackingRecordEntity.Id = int.Parse(idValueFromText);
                    trackingRecordEntity.StartDateTime = DateTime.Parse(dateStartValueFromText);
                    trackingRecordEntity.ActivityName = nameValueFromText;
                    if (!string.IsNullOrWhiteSpace(dateEndValueFromText))
                    {
                        trackingRecordEntity.EndDateTime = DateTime.Parse(dateEndValueFromText);
                    }    
                    
                    return trackingRecordEntity;
                }
            }

            return null;            
        }

        public string GetTextFormatFromTrackingRecord(ITrackingRecordEntity trackingRecordEntity, int id)
        {
            string idFormat = id.ToString();
            string nameFormat = trackingRecordEntity.ActivityName.Replace("|", string.Empty);
            string startDateformat = trackingRecordEntity.StartDateTime.ToString(CultureInfo.InvariantCulture);
            string endDateFormat = string.Empty;
            if (trackingRecordEntity.EndDateTime.HasValue)
            {
                endDateFormat = trackingRecordEntity.EndDateTime.Value.ToString(CultureInfo.InvariantCulture);
            }
            return $"{idFormat}|{nameFormat}|{startDateformat}|{endDateFormat}";
        }
    }

    internal interface ITextTrackingRecordFormatValidator
    {
    }
}