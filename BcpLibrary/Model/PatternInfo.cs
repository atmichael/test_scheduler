using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibrary.Model
{
    public class PatternInfo
    {
        public int patternCount { get; private set; }
        public string defaultMode { get; private set; }
        public string alternateMode { get; private set; }

        public PatternInfo(int count, string defValue, string altValue)
        {
            patternCount = count;
            defaultMode = defValue;
            alternateMode = altValue;
        }
    }
}
