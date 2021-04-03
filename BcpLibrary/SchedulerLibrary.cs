using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibrary
{
    public class SchedulerLibrary : ISchedulerLibrary
    {
        public int GetNumberOfPatterns(decimal targetPercent)
        {
            if (targetPercent == 0 || targetPercent == 100)
            {
                return 1;
            }
            if (targetPercent >= 0 && targetPercent <= 100)
            {
                decimal percentOfWorkFromHome = 100 - targetPercent;
                decimal rawFactor = 100 / percentOfWorkFromHome;                
                int workFromHomeFactor = Convert.ToInt32(Math.Ceiling(100 / percentOfWorkFromHome));

                decimal effectivePercent = 100 - (100 / workFromHomeFactor); 
                while (effectivePercent > targetPercent)
                {
                    // reducing the pattern count will result in lower effective percent
                    workFromHomeFactor = workFromHomeFactor - 1; 
                    effectivePercent = 100 - (100 / workFromHomeFactor);
                }
                

                return workFromHomeFactor;
            }
            else
            {
                throw new Exception("Invalid target percent value. must be between 0 and 100"); 
            }
        }
    }
}
