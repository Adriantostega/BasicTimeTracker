namespace BasicTimeTracker.Util
{
    public interface IDirectoryManager
    {
        void CreateIfDoesNotExist(string pathDirectory);

        string[] GetAllFilesName(string pathDirectory);
    }
}