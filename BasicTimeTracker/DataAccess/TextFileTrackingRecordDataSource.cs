using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BasicTimeTracker.Model;
using BasicTimeTracker.Util;

namespace BasicTimeTracker.DataAccess
{
    public class TextFileTrackingRecordDataSource : ITrackingRecordDataSource
    {
        public IFileManager FileManager { get; private set; }
        public string FolderPath { get; private set; }        
        public IDirectoryManager DirectoryManager { get; private set; }
        public ITextLineReader TextLineReader { get; private set; }
        public ITextFileTrackingRecordFormatManager TextFileTrackingRecordFormatManager { get; private set; }
        public ITextFileTrackingRecordCreator TextFileTrackingRecordCreator { get; private set; }
        public ITextFileTrackingRecordFileManager TextFileTrackingRecordFileManager { get; private set; }
        public TextFileTrackingRecordDataSource(IFileManager fileManager, IDirectoryManager directoryManager, ITextLineReader textLineReader, ITextFileTrackingRecordFormatManager textFileTrackingRecordFormatManager, ITextFileTrackingRecordCreator textFileTrackingRecordCreator, ITextFileTrackingRecordFileManager textFileTrackingRecordFileManager) : this(fileManager, null, directoryManager, textLineReader, textFileTrackingRecordFormatManager, textFileTrackingRecordCreator, textFileTrackingRecordFileManager) {}

        public TextFileTrackingRecordDataSource(IFileManager fileManager, string folderPath, IDirectoryManager directoryManager, ITextLineReader textLineReader, ITextFileTrackingRecordFormatManager textFileTrackingRecordFormatManager, ITextFileTrackingRecordCreator textFileTrackingRecordCreator, ITextFileTrackingRecordFileManager textFileTrackingRecordFileManager)
        {
            FileManager = fileManager;            
            DirectoryManager = directoryManager;
            FolderPath = folderPath;
            TextLineReader = textLineReader;
            TextFileTrackingRecordFormatManager = textFileTrackingRecordFormatManager;
            TextFileTrackingRecordCreator = textFileTrackingRecordCreator;
            TextFileTrackingRecordFileManager = textFileTrackingRecordFileManager;

            InitializeTextFileTrackingDataSource(folderPath);
        }

        private void InitializeTextFileTrackingDataSource(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                FolderPath = LocalPath.Instance().PathDirectory;
            }
            DirectoryManager.CreateIfDoesNotExist(FolderPath);            
        }        

        public List<ITrackingRecordEntity> GetAllRecords()
        {
            var trackingRecordEntities = new List<ITrackingRecordEntity>();
            var allFilesName = DirectoryManager.GetAllFilesName(FolderPath);

            foreach (var fileName in allFilesName)
            {
                trackingRecordEntities.AddRange(TextFileTrackingRecordCreator.CreateFromFile(fileName));                
            }
                        
            return trackingRecordEntities;
        }      

        private int GetNewId()
        {
            var trackingRecordEntities = GetAllRecords();
            var lastOrDefault = trackingRecordEntities.OrderBy(re => re.Id).LastOrDefault();

            if (lastOrDefault == null)
            {
                return 1;
            }
            else
            {
                return lastOrDefault.Id + 1;
            }
        }

        public TrackingRecordEntity Create(TrackingRecordEntity recordEntity)
        {
            var fileNameFromToday = TextFileTrackingRecordNaming.Instance().GenerateNameFromDate(DateTime.Now, "txt");
            var fileNameFromTodayWithPath = Path.Combine(FolderPath, fileNameFromToday);
            FileManager.CreateIfDoesNotExist(fileNameFromTodayWithPath);
            int Id = GetNewId();
            var textFormatFromTrackingRecord = TextFileTrackingRecordFormatManager.GetTextFormatFromTrackingRecord(recordEntity, Id);
            FileManager.AppendNewLine(textFormatFromTrackingRecord, fileNameFromTodayWithPath);
            recordEntity.Id = Id;
            return recordEntity;
        }

        public void Update(ITrackingRecordEntity recordEntity)
        {
            var trackingRecordEntities = GetAllRecords();
            var trackingRecordEntity = trackingRecordEntities.FirstOrDefault(te => te.Id == recordEntity.Id);
            var textTrackingRecordEntity = trackingRecordEntity as TextTrackingRecordEntity;
            if (textTrackingRecordEntity != null)
            {
                var fileNameWithPath = textTrackingRecordEntity.FileNameWithPath;

                var trackingRecordEntitiesFromFile = TextFileTrackingRecordCreator.CreateFromFile(fileNameWithPath);
                var trackingRecordEntityToBeUpdated = trackingRecordEntitiesFromFile.FirstOrDefault(tre => tre.Id == recordEntity.Id);
                trackingRecordEntitiesFromFile.Remove(trackingRecordEntityToBeUpdated);
                trackingRecordEntitiesFromFile.Add(recordEntity);

                TextFileTrackingRecordFileManager.UpdateContentOfFile(fileNameWithPath, trackingRecordEntitiesFromFile);                
            }
        }        

        public void Delete(int id)
        {
            var trackingRecordEntity = GetAllRecords().FirstOrDefault(te => te.Id == id);
            var textTrackingRecordEntity = trackingRecordEntity as TextTrackingRecordEntity;
            if (textTrackingRecordEntity != null)
            {
                var fileName = textTrackingRecordEntity.FileNameWithPath;

                var trackingRecordEntitiesFromFile = TextFileTrackingRecordCreator.CreateFromFile(fileName);
                var trackingRecordEntityToBeUpdated = trackingRecordEntitiesFromFile.FirstOrDefault(tre => tre.Id == id);
                trackingRecordEntitiesFromFile.Remove(trackingRecordEntityToBeUpdated);

                TextFileTrackingRecordFileManager.UpdateContentOfFile(fileName, trackingRecordEntitiesFromFile);
            }
        }       
    }
}
