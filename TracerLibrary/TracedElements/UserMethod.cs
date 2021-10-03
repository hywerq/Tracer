using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLibrary
{
    [XmlType("Method")]
    public class UserMethod
    {
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

        public UserMethod()
        {
            IsActive = true;
            StartTime = DateTime.Now;
            Time = 0;
        }
    }
}
