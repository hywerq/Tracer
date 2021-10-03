using System;
using System.Threading;
using TracerLibrary;

namespace TracerTest
{
    class ConsoleTest
    {
        public static void NewMethod(object o)
        {
            Tracer tracer = (Tracer)o;

            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }

        static void Main(string[] args)
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            //foo.MyMethod();

            Thread thread = new Thread(new ParameterizedThreadStart(NewMethod));
            thread.Start(tracer);
            thread.Join();

            tracer.ShowTraceResult();

            JSONSerializer jsonSerializer = new JSONSerializer();
            jsonSerializer.SerializeAndExport(tracer.GetTraceResult());
            jsonSerializer.OpenFile();

            XMLSerializer xmlSerializer = new XMLSerializer();
            xmlSerializer.SerializeAndExport(tracer.GetTraceResult());
            xmlSerializer.OpenFile();

            Console.ReadKey();
        }
    }
}
