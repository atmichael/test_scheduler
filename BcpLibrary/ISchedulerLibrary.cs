using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibrary
{
    public interface ISchedulerLibrary
    {
         int GetNumberOfPatterns(decimal targetPercent); 
    }
}
