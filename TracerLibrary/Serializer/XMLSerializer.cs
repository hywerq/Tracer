using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace TracerLibrary
{
    public class XMLSerializer : ISerializer
    {
        private XmlSerializer _serializer;
        private readonly string _path = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory) + "\\testXML.txt";

        public void SerializeAndExport(TraceResult traceResult)
        {
            _serializer = new XmlSerializer(typeof(TraceResult));

            File.WriteAllText($"{_path}", string.Empty);
            using (FileStream fstream = new FileStream($"{_path}", FileMode.OpenOrCreate))
            {
                _serializer.Serialize(fstream, traceResult);
            }
        }

        public void OpenFile()
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo($"{_path}")
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }
    }
}
