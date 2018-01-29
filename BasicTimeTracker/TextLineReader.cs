using System;
using System.Collections.Generic;
using System.IO;
using BasicTimeTracker.DataAccess;

namespace BasicTimeTracker
{
    public class TextLineReader : ITextLineReader
    {
        public string TextContent { get; private set; }
        public void SetText(string textContext)
        {
            TextContent = textContext;
        }

        public List<string> GetLines()
        {
            List<string> allLines = new List<string>();
            using (StringReader reader = new StringReader(TextContent))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        allLines.Add(line);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return allLines;
        }
    }
}