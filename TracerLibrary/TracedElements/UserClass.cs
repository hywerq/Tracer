using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLibrary
{
    [XmlType("Class")]
    public class UserClass
    {
        public List<UserMethod> MethodList;

        [XmlAttribute]
        public string Name;

        [XmlIgnore]
        [JsonIgnore]
        public DateTime StartTime;

        [XmlAttribute]
        public double Time;

        [XmlIgnore]
        [JsonIgnore]
        public bool IsActive;

        public UserClass()
        {
            MethodList = new List<UserMethod>();
            IsActive = true;
            StartTime = DateTime.Now;
            Time = 0;
        }

        public bool HasActiveMethods()
        {
            foreach (UserMethod methodItem in MethodList)
            {
                if (methodItem.IsActive)
                {
                    return true;
                }
            }
            return false;
        }

        public UserMethod GetMethodByName(string methodName)
        {
            foreach (UserMethod methodItem in MethodList)
            {
                if (methodItem.Name.Equals(methodName))
                {
                    return methodItem;
                }
            }
            return null;
        }
    }
}
