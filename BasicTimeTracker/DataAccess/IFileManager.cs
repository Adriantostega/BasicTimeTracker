namespace BasicTimeTracker.DataAccess
{
    public interface IFileManager
    {
        string ReadAllText(string fileNameWithPath);
        void CreateIfDoesNotExist(string fileNameWithPath);
        void AppendNewLine(string newLine, string fileNameWithPath);
        void Delete(string fileNameWithPath);
        void Rename(string fileNameFromTodayTemporary, string newfileNameWithPath);
    }
}