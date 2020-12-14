using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Opt
    {
        public string SourceDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public string LogDirectory { get; set; }

        public Opt(string _sourceDirectory, string _targetDirectory, string _logDirectory)
        {
            SourceDirectory = _sourceDirectory;
            TargetDirectory = _targetDirectory;
            LogDirectory = _logDirectory;
        }
    }
}
