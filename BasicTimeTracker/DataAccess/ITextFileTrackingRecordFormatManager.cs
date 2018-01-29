using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public interface ITextFileTrackingRecordFormatManager
    {
        bool IsFormatCorrect(string text);
        TextTrackingRecordEntity GetTrackingRecordFromText(string text);
        string GetTextFormatFromTrackingRecord(ITrackingRecordEntity trackingRecordEntity, int id);
    }
}