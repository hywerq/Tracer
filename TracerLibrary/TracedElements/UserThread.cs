using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLibrary
{
    [XmlType("Thread")]
    public class UserThread
    {
        public List<UserClass> ClassList;

        [XmlAttribute]
        public int Id;

        [XmlIgnore]
        [JsonIgnore]
        public DateTime StartTime;

        [XmlAttribute]
        public double Time;

        [XmlIgnore]
        [JsonIgnore]
        public bool IsActive;

        public UserThread()
        {
            ClassList = new List<UserClass>();
            IsActive = true;
            StartTime = DateTime.Now;
            Time = 0;
        }

        public bool HasActiveClasses()
        {
            foreach (UserClass classItem in ClassList)
            {
                if (classItem.IsActive)
                {
                    return true;
                }
            }
            return false;
        }

        public UserClass GetClassByName(string className)
        {
            foreach (UserClass classItem in ClassList)
            {
                if (classItem.Name.Equals(className))
                {
                    return classItem;
                }
            }
            return null;
        }
    }
}
