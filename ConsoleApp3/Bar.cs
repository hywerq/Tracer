using System.Threading;
using TracerLibrary;

namespace TracerTest
{
    public class Bar
    {
        private ITracer _tracer;

        public Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(50);

            _tracer.StopTrace();
        }
    }
}
