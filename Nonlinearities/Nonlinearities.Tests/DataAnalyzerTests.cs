using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonlinearities.Analysis;
using System.Globalization;

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
            var deviation = new double[2][];

            var staRounded = STA.Calculate(stimuli, spikes, RoundStrategy.Round);
            var values = String.Join(", ", Array.ConvertAll(staRounded, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (round strategy) = [" + values + "]");

            var staCeiled = STA.Calculate(stimuli, spikes, RoundStrategy.Ceiling);
            values = String.Join(", ", Array.ConvertAll(staCeiled, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[0] = Analysis.Math.Subtract(staCeiled, staRounded);
            Console.WriteLine("STA (ceiling strategy) = [" + values + "]");

            var staFloored = STA.Calculate(stimuli, spikes, RoundStrategy.Floor);
            values = String.Join(", ", Array.ConvertAll(staFloored, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[1] = Analysis.Math.Subtract(staFloored, staRounded);
            Console.WriteLine("STA (floor strategy) = [" + values + "]");

            var averageDeviation = Analysis.Math.Mean(deviation);
            values = String.Join(", ", Array.ConvertAll(averageDeviation, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("Average Deviation = [" + values + "]");
            
        }
    }
}
