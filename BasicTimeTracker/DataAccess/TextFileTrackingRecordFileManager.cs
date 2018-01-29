using System;
using System.Collections.Generic;
using System.IO;
using BasicTimeTracker.Model;
using BasicTimeTracker.Util;

namespace BasicTimeTracker.DataAccess
{
    public class TextFileTrackingRecordFileManager : ITextFileTrackingRecordFileManager
    {
        public IFileManager FileManager { get; private set; }        
        public ITextFileTrackingRecordFormatManager TextFileTrackingRecordFormatManager { get; private set; }

        public TextFileTrackingRecordFileManager(IFileManager fileManager, ITextFileTrackingRecordFormatManager textFileTrackingRecordFormatManager)
        {
            FileManager = fileManager;
            TextFileTrackingRecordFormatManager = textFileTrackingRecordFormatManager;
        }
        public void UpdateContentOfFile(string fileNameWithPathToUpdate, List<ITrackingRecordEntity> trackingRecordEntitiesFromFile)
        {
            var fileNameFromTodayTemporary = TextFileTrackingRecordNaming.Instance().GenerateNameFromDateAndtime(DateTime.Now, "txt");
            var pathDirectory = LocalPath.Instance().PathDirectory;
            var temporaryFileNameWithPath = Path.Combine(pathDirectory, fileNameFromTodayTemporary);
            FileManager.CreateIfDoesNotExist(temporaryFileNameWithPath);

            foreach (var entity in trackingRecordEntitiesFromFile)
            {
                var textFormatFromTrackingRecord = TextFileTrackingRecordFormatManager.GetTextFormatFromTrackingRecord(entity, entity.Id);
                FileManager.AppendNewLine(textFormatFromTrackingRecord, temporaryFileNameWithPath);
            }

            FileManager.Delete(fileNameWithPathToUpdate);
            FileManager.Rename(temporaryFileNameWithPath, fileNameWithPathToUpdate);
        }
    }
}