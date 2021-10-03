namespace TracerLibrary
{
    public interface ISerializer
    {
        void SerializeAndExport(TraceResult traceResult);

        void OpenFile();
    }
}
