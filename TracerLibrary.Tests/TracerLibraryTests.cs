using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TracerTest;

namespace TracerLibrary.Tests
{
    [TestClass]
    public class TracerLibraryTests
    {
        private ITracer _tracer = new Tracer();

        [TestMethod]
        public void TraceElementsAreStoredCorrectly()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();

            TraceResult traceResult = _tracer.GetTraceResult();

            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, 
                traceResult.ThreadList[0].Id);
            Assert.AreEqual(nameof(TracerLibraryTests),
                traceResult.ThreadList[0].ClassList[0].Name);
            Assert.AreEqual(nameof(TraceElementsAreStoredCorrectly),
                traceResult.ThreadList[0].ClassList[0].MethodList[0].Name);
        }

        [TestMethod]
        public void When_OneMethodIsCalledTwice_Expect_AdditionOfExecutionTime()
        {
            double oneMethodTime, twoMethodsTime;
            Foo foo = new Foo(_tracer);
            foo.MyMethod();

            TraceResult traceResult = _tracer.GetTraceResult();
            oneMethodTime = traceResult.ThreadList[0].ClassList[0].MethodList[0].Time;

            foo.MyMethod();

            twoMethodsTime = traceResult.ThreadList[0].ClassList[0].MethodList[0].Time;

            Assert.IsTrue(oneMethodTime < twoMethodsTime);
        }
    }
}
