using BcpLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibrary
{
    public interface ISchedulerLibrary
    {
         PatternInfo GetPatternInfo(decimal targetPercent, int populationSize); 
    }
}
