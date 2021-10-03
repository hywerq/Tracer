using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TracerLibrary
{
    public class TraceResult
    {
        public List<UserThread> ThreadList { get; private set; }

        public TraceResult () { }

        public TraceResult(ConcurrentBag<UserThread> tracingList)
        {
            ThreadList = tracingList.ToList();
        }
    }
}
