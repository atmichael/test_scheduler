using BcpLibrary.Model;
using System;

namespace BcpLibrary
{
    public class SchedulerLibrary : ISchedulerLibrary
    {
        public PatternInfo GetPatternInfo(decimal targetPercent, int populationSize)
        {
            if ((populationSize >= 0 && populationSize <= 1) ||
                targetPercent == 100)
            {
                return new PatternInfo(1, "O", "H");
            }
            else if (targetPercent == 0)
            {
                return new PatternInfo(1, "H", "0");
            }
            if (targetPercent >= 50 && targetPercent <= 100)
            {
                decimal percentOfWorkFromHome = 100 - targetPercent;
                int workFromHomeCount = Convert.ToInt32(Math.Ceiling(populationSize * percentOfWorkFromHome / 100));
                int workFromHomeFactor = Convert.ToInt32(Math.Ceiling(Decimal.Divide(populationSize, workFromHomeCount)));

                decimal effectivePercent = 100 - (100 / workFromHomeFactor);
                while (effectivePercent > targetPercent)
                {
                    // reducing the pattern count will result in lower effective percent
                    workFromHomeFactor = workFromHomeFactor - 1;
                    effectivePercent = 100 - (100 / workFromHomeFactor);
                }

                return new PatternInfo(workFromHomeFactor, "O", "H");
            }
            else if (targetPercent >= 0 && targetPercent < 50)
            {
                int workFromOfficeCount = Convert.ToInt32(Math.Floor(populationSize * targetPercent / 100));
                int workFromOfficeFactor = Convert.ToInt32(Math.Ceiling(decimal.Divide(populationSize, workFromOfficeCount)));
                decimal effectivePercent = (100 / workFromOfficeFactor);
                while (effectivePercent > targetPercent)
                {
                    // reducing the pattern count will result in lower effective percent
                    workFromOfficeFactor = workFromOfficeFactor - 1;
                    effectivePercent = (100 / workFromOfficeFactor);
                }
                return new PatternInfo(workFromOfficeFactor, "H", "O");
            }
            else
            {
                throw new Exception("Invalid target percent value. must be between 0 and 100");
            }
        }
    }
}
