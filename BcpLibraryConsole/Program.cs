using BcpLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibraryConsole
{
    class Program
    {
        static int m_numberOfWeeks; 
        static decimal? m_target = null; 
        static void Main(string[] args)
        {
            InitializeNumberOfWeekInCurrentYear();

            GetTargetValueFromInput();

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
                    bool isWorkFromHomeDay = ((weekIndex - offset) % factor) == 0;
                    patternContent[weekIndex + 1] = isWorkFromHomeDay? "H": "O";
                }
                Console.WriteLine(string.Join("|", patternContent));
            }

            Console.WriteLine();
            Console.WriteLine("Schedule Generated!");
            Console.ReadLine();
        }

        private static int GetNumberOfPatternsToGenerate()
        {
            ISchedulerLibrary lib = new SchedulerLibrary();

            int factor = lib.GetNumberOfPatterns(m_target.Value);

            Console.WriteLine(string.Format("Number of patterns to generate : {0}", factor));
            decimal effective = 100 - (100 / factor);
            Console.WriteLine(string.Format("Max number of people in office will be : {0:N2}%", effective));
            return factor;
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
                    
                } else
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
