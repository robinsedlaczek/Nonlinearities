using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonlinearities.Analysis;

namespace Nonlinearities.Tests
{
    [TestClass]
    public class DataAnalyzerTests
    {
        [TestMethod]
        public void TestOpenFile()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            
        }
    }
}
.