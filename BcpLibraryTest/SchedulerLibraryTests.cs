using Microsoft.VisualStudio.TestTools.UnitTesting;
using BcpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BcpLibrary.Model;

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
                PatternInfo info = m_lib.GetPatternInfo(-1, 100);
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
                PatternInfo info = m_lib.GetPatternInfo((decimal)100.01, 100);
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
            PatternInfo info = m_lib.GetPatternInfo(0, 100);
            Assert.AreEqual(1, info.patternCount);
            Assert.AreEqual("H", info.defaultMode);
            Assert.AreEqual("O", info.alternateMode);
        }

        [TestMethod()]
        public void Assert_ReturnOneIf100()
        {
            PatternInfo info = m_lib.GetPatternInfo(100, 100);
            Assert.AreEqual(1, info.patternCount);
            Assert.AreEqual("O", info.defaultMode);
            Assert.AreEqual("H", info.alternateMode);
        }

        [TestMethod()]
        public void Assert_Return4If75()
        {
            PatternInfo expected = new PatternInfo(4, "O", "H"); 
            AssertReturnValueWhenTarget((decimal)75.0, 100, expected);
        }

        [TestMethod()]
        public void Assert_Return5If80()
        {
            PatternInfo expected = new PatternInfo(5, "O", "H");
            AssertReturnValueWhenTarget((decimal)80.0, 100, expected); 
        }


        [TestMethod()]
        public void Assert_Returns4If79()
        {
            PatternInfo expected = new PatternInfo(4, "O", "H");
            AssertReturnValueWhenTarget((decimal)79.0, 100, expected);
        }

        [TestMethod()]
        public void Assert_Returns4If76()
        {
            PatternInfo expected = new PatternInfo(4, "O", "H");
            AssertReturnValueWhenTarget((decimal)76.0, 100, expected);
        }

        [TestMethod()]
        public void Assert_Returns6If85()
        {
            PatternInfo expected = new PatternInfo(6, "O", "H");
            AssertReturnValueWhenTarget((decimal)85.0, 100, expected);
        }

        [TestMethod()]
        public void Assert_Returns3If34()
        {
            PatternInfo expected = new PatternInfo(3, "H", "O");
            AssertReturnValueWhenTarget((decimal)34.0, 200, expected);
        }


        private void AssertReturnValueWhenTarget(decimal target, int populationSize, PatternInfo returnValue)
        {
            PatternInfo info = m_lib.GetPatternInfo(target, populationSize);
            Assert.AreEqual(returnValue.patternCount, info.patternCount);
            Assert.AreEqual(returnValue.defaultMode, info.defaultMode);
            Assert.AreEqual(returnValue.alternateMode, info.alternateMode);
            if (returnValue.defaultMode.Equals("O"))
            {
                Assert.IsTrue((1 / returnValue.patternCount) <= target);
            }
            else
            {
                Assert.IsTrue((1 - (1 / returnValue.patternCount)) <= target);
            }
        }

    }
}