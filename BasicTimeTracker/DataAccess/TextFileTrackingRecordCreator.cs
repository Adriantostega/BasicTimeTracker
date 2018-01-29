using System.Collections.Generic;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public class TextFileTrackingRecordCreator : ITextFileTrackingRecordCreator
    {
        public IFileManager FileManager { get; private set; }        
        public ITextLineReader TextLineReader { get; private set; }
        public ITextFileTrackingRecordFormatManager TextFileTrackingRecordFormatManager { get; private set; }

        public TextFileTrackingRecordCreator(IFileManager fileManager, ITextLineReader textLineReader, ITextFileTrackingRecordFormatManager textFileTrackingRecordFormatManager)
        {
            FileManager = fileManager;                        
            TextLineReader = textLineReader;
            TextFileTrackingRecordFormatManager = textFileTrackingRecordFormatManager;
        }

        public List<ITrackingRecordEntity> CreateFromFile(string fileNameWithPath)
        {
            var trackingRecordEntities = new List<ITrackingRecordEntity>();            
            var textContext = FileManager.ReadAllText(fileNameWithPath);

            TextLineReader.SetText(textContext);
            var allLines = TextLineReader.GetLines();

            foreach (var line in allLines)
            {
                if (TextFileTrackingRecordFormatManager.IsFormatCorrect(line))
                {
                    var trackingRecordFromText = TextFileTrackingRecordFormatManager.GetTrackingRecordFromText(line);
                    trackingRecordFromText.FileNameWithPath = fileNameWithPath;
                    trackingRecordEntities.Add(trackingRecordFromText);
                }
            }
            return trackingRecordEntities;
        }

    }
}