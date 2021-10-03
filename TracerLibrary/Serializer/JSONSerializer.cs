using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TracerLibrary
{
    public class JSONSerializer : ISerializer
    {
        private readonly string _path = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory) + "\\testJSON.txt";
        
        public void SerializeAndExport(TraceResult traceResult)
        {
            File.WriteAllText($"{_path}", string.Empty);
            using (FileStream fstream = new FileStream($"{_path}", FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.Default.GetBytes(JsonConvert.SerializeObject(traceResult, Formatting.Indented));
                fstream.Write(array, 0, array.Length);
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
