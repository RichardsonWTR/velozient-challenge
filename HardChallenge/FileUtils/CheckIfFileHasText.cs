using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    public class CheckIfFileHasText
    {
        private readonly string _filename;

        public CheckIfFileHasText(string fileName)
        {
            _filename = fileName;
        }

        public bool HasText(string textToSearch)
        {
            var output = File.ReadLines(_filename).
                Where(line => line.Contains(textToSearch)).
                FirstOrDefault();
            return output != null ? output.Any() : false;
        }
    }
}
