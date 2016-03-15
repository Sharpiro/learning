using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.Core
{
    public static class CsvReader
    {
        public static IEnumerable<Dictionary<string, object>> ReadFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    var lines = new List<Dictionary<string, object>>();
                    var splitHeader = reader.ReadLine()?.ToLower().Split(',').Select(h => h.Trim()).ToList();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var split = line.Split(',').Select(s => s.Trim()).ToList();
                        var dict = new Dictionary<string, object>();
                        for (var i = 0; i < splitHeader?.Count; i++)
                        {
                            dict.Add(splitHeader[i], split[i]);
                        }
                        lines.Add(dict);
                    }
                    return lines;
                }
            }
        }
    }
}
