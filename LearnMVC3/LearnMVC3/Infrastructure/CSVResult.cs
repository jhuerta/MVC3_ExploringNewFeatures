using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LearnMVC3.Infrastructure
{
    public class CSVResult: System.Web.Mvc.FileResult
    {
        private IEnumerable<dynamic> _data;
        public CSVResult(IEnumerable<dynamic> data, string fileName ) : base("text/csv")
        {
            _data = data;
            this.FileDownloadName = fileName;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            using(var stream = new MemoryStream())
            {
                WriteData(stream);

                response.OutputStream.Write(stream.GetBuffer(),0,(int) stream.Length);
            }
        }

        private void WriteData(MemoryStream stream)
        {
            var writer = new StreamWriter(stream);

            var first = _data.First();
            var dictionary = (IDictionary<string, object>) first;
            foreach (var key in dictionary.Keys)
            {
                writer.Write(WriteVal(key));
            }
            writer.WriteLine();

            foreach (var line in _data)
            {
                dictionary = (IDictionary<string, object>) line;
                var values = dictionary.Values;
                foreach (var v in values)
                {
                    writer.Write(WriteVal( (v ?? "").ToString()));
                }
                writer.WriteLine();
            }
            writer.Flush();
        }

        string WriteVal(string val)
        {
            return string.Format("\"{0}\",", val.Replace("\"", "\"\""));
            
        }
    }
}