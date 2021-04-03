using BcpLibrary;
using BcpLibrary.Model;
using System;
using System.Globalization;

namespace BcpLibraryConsole
{
    class Program
    {
        static int m_numberOfWeeks;
        static decimal? m_target = null;
        static int? m_populationSize = null;
        static bool m_isDefaultWorkFromHome;
        static string m_default;
        static string m_alternate;
        static void Main(string[] args)
        {
            InitializeNumberOfWeekInCurrentYear();

            GetTargetValueFromInput();
            GetPopulationSizeFromInput();

            int factor = GetNumberOfPatternsToGenerate();

            // Print week header
            string[] weekHeaders = new string[m_numberOfWeeks + 1];
            weekHeaders[0] = "Week Number";
            for (int weekIndex = 0; weekIndex < m_numberOfWeeks; weekIndex++)
            {
                weekHeaders[weekIndex + 1] = weekIndex.ToString();
            }
            Console.WriteLine(string.Join("|", weekHeaders));


            for (int factorIndex = 0; factorIndex < factor; factorIndex++)
            {
                string[] patternContent = new string[m_numberOfWeeks + 1];
                patternContent[0] = string.Format("pattern {0}", factorIndex + 1);
                int offset = factorIndex;
                for (int weekIndex = 0; weekIndex < m_numberOfWeeks; weekIndex++)
                {
                    bool isDueForAlternate = ((weekIndex - offset) % factor) == 0;
                    patternContent[weekIndex + 1] = isDueForAlternate ? m_alternate : m_default;
                }
                Console.WriteLine(string.Join("|", patternContent));
            }

            Console.WriteLine();
            Console.WriteLine("Schedule Generated!");
            Console.ReadLine();
        }

        private static void GetPopulationSizeFromInput()
        {
            while (!m_populationSize.HasValue)
            {
                Console.WriteLine(Messages.prompt_getPopulationSize);
                string inputvalue = Console.ReadLine();

                if (int.TryParse(inputvalue, out var target))
                {
                    if (target > 0)
                    {
                        m_populationSize = target;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a positive number");
                    }

                }
                else
                {
                    Console.WriteLine("Please enter a positive number");
                }
            }
            Console.WriteLine(string.Format("Population Size is : {0}", m_populationSize));
        }

        private static int GetNumberOfPatternsToGenerate()
        {
            ISchedulerLibrary lib = new SchedulerLibrary();

            PatternInfo pattern = lib.GetPatternInfo(m_target.Value, m_populationSize.Value);

            m_isDefaultWorkFromHome = pattern.defaultMode.Equals("H");
            m_default = pattern.defaultMode;
            m_alternate = pattern.alternateMode;

            Console.WriteLine(string.Format("Number of patterns to generate : {0}", Math.Abs(pattern.patternCount)));
            Console.WriteLine(string.Format("Default working mode is (O for office, H for Home): {0}", m_default));
            decimal effective = m_isDefaultWorkFromHome ? (100 / Math.Abs(pattern.patternCount)) : 100 - (100 / pattern.patternCount);

            Console.WriteLine(string.Format("Max number of people in office will be : {0:N2}%", effective));
            return Math.Abs(pattern.patternCount);
        }

        private static void GetTargetValueFromInput()
        {
            while (!m_target.HasValue)
            {
                Console.WriteLine(Messages.prompt_getTargetWFO);
                string inputvalue = Console.ReadLine();

                if (decimal.TryParse(inputvalue, out var target))
                {
                    if (target >= 0 && target <= 100)
                    {
                        m_target = target;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number between 0.00 to 100.00");
                    }

                }
                else
                {
                    Console.WriteLine("Please enter a number between 0.00 to 100.00");
                }
            }
            Console.WriteLine(string.Format("Target percentage of Work from Office is : {0:N2}", m_target));
        }

        private static void InitializeNumberOfWeekInCurrentYear()
        {
            DateTime today = DateTime.Now;
            DateTime endOfTheYear = new DateTime(today.Year, 12, 31);

            CultureInfo info = new CultureInfo("en-US");
            Calendar cal = info.Calendar;
            m_numberOfWeeks = cal.GetWeekOfYear(endOfTheYear, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

    }
}
