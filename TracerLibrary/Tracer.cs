using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        public string MethodName;
        public string ClassName;
        public int ThreadId;
        private DateTime _endTime;
        //private List<UserThread> _traicingList;
        private ConcurrentBag<UserThread> _traicingList;

        public Tracer() 
        {
            //_traicingList = new List<UserThread>();
            _traicingList = new ConcurrentBag<UserThread>();
        }

        public void ShowTraceResult()
        {
            foreach (UserThread thread in _traicingList)
            {
                Console.WriteLine("Thread id: " + thread.Id + ", time: " + thread.Time);
                foreach (UserClass userClass in thread.ClassList)
                {
                    Console.WriteLine("- Class name: " + userClass.Name + ", time: " + userClass.Time);
                    foreach (UserMethod method in userClass.MethodList)
                    {
                        Console.WriteLine("   - Method name: " + method.Name + ", time: " + method.Time);
                    }
                }
                Console.WriteLine();
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_traicingList);
        }

        private UserThread GetThreadById(int id)
        {
            foreach (UserThread threadItem in _traicingList)
            {
                if (threadItem.Id.Equals(id))
                {
                    return threadItem;
                }
            }
            return null;
        }

        public void StartTrace()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;

            UserThread myThread = GetThreadById(ThreadId);
            if (myThread == null)
            {
                myThread = new UserThread() { Id = ThreadId };
                _traicingList.Add(myThread);
            }

            ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;

            UserClass myClass = myThread.GetClassByName(ClassName);
            if (myClass == null)
            {
                myClass = new UserClass() { Name = ClassName };
                myThread.ClassList.Add(myClass);
            }

            MethodName = new StackTrace().GetFrame(1).GetMethod().Name;

            UserMethod myMethod = myClass.GetMethodByName(MethodName);
            if (myMethod == null)
            {
                myMethod = new UserMethod() { Name = MethodName };
                myClass.MethodList.Add(myMethod);
            }

            myMethod.StartTime = DateTime.Now;
        }

        public void StopTrace()
        {
            _endTime = DateTime.Now;

            MethodName = new StackTrace().GetFrame(1).GetMethod().Name;
            ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            ThreadId = Thread.CurrentThread.ManagedThreadId;

            UserThread myThread = GetThreadById(ThreadId);
            UserClass myClass = myThread.GetClassByName(ClassName);
            UserMethod myMethod = myClass.GetMethodByName(MethodName);

            myMethod.Time += Math.Round((_endTime - myMethod.StartTime).TotalMilliseconds, 4);
            myMethod.IsActive = false;

            if (!myClass.HasActiveMethods())
            {
                myClass.Time += Math.Round((DateTime.Now - myClass.StartTime).TotalMilliseconds, 4);
                myClass.IsActive = false;

                if (!myThread.HasActiveClasses())
                {
                    myThread.Time = Math.Round((DateTime.Now - myThread.StartTime).TotalMilliseconds, 4);
                    myThread.IsActive = false;
                }
            }
        }
    }
}
