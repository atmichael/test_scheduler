using Microsoft.VisualStudio.TestTools.UnitTesting;
using BcpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcpLibrary.Tests
{
    [TestClass()]
    public class SchedulerLibraryTests
    {
        ISchedulerLibrary m_lib; 

        [TestInitialize]
        public void  InitializeLib()
        {
            m_lib = new SchedulerLibrary(); 
        }

        [TestMethod()]
        public void Assert_ThrowErrorIfLessThanZero()
        {
            // when 
            string error = "";
            try
            {
                int numberOfPatterns = m_lib.GetNumberOfPatterns(-1);
            }
            catch(Exception ex)
            {
                error = ex.Message; 
            }
            // then
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod()] 
        public void Assert_ThrowErrorIfMoreThan100()
        {
            // when 
            string error = "";
            try
            {
                int numberOfPatterns = m_lib.GetNumberOfPatterns((decimal)100.01);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            // then
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod()]
        public void Assert_ReturnOneIfZero()
        {
            int numberOfPatterns = m_lib.GetNumberOfPatterns(0);
            Assert.AreEqual(1, numberOfPatterns); 
        }

        [TestMethod()]
        public void Assert_ReturnOneIf100()
        {
            int numberOfPatterns = m_lib.GetNumberOfPatterns(100);
            Assert.AreEqual(1, numberOfPatterns);
        }

        [TestMethod()]
        public void Assert_Return4If75()
        {
            AssertReturnValueWhenTarget((decimal)75.0, 4);
        }

        [TestMethod()]
        public void Assert_Return5If80()
        {
            AssertReturnValueWhenTarget((decimal)80.0, 5); 
        }


        [TestMethod()]
        public void Assert_Returns4If79()
        {
            AssertReturnValueWhenTarget((decimal)79.0, 4);
        }

        [TestMethod()]
        public void Assert_Returns4If76()
        {
            AssertReturnValueWhenTarget((decimal)76.0, 4);
        }

        [TestMethod()]
        public void Assert_Returns6If85()
        {
            AssertReturnValueWhenTarget((decimal)85.0, 6);
        }

        private void AssertReturnValueWhenTarget(decimal target, int returnValue)
        {
            int numberOfPatterns = m_lib.GetNumberOfPatterns(target);
            Assert.AreEqual(returnValue, numberOfPatterns);
        }

    }
}