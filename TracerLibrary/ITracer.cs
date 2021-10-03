namespace TracerLibrary
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        void ShowTraceResult();

        TraceResult GetTraceResult();
    }
}
