namespace BasicTimeTracker.DataAccess
{
    public interface IDataWriter
    {
        string Read();
        void Write(string value);
    }
}