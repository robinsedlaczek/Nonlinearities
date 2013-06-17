using System;
using System.Diagnostics;
using AForge.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonlinearities.Analysis;
using System.Globalization;
using Math = Nonlinearities.Analysis.Math;

namespace Nonlinearities.Tests
{
    [TestClass]
    public class DataAnalyzerTests
    {
        [TestMethod]
        public void TestCalculateSTA_DifferentRoundingStrategies()
        {
            Console.WriteLine("Test calculation of STA with different rounding strategies. First cell from spike data is used.\n");

            var stimuli = DataLoader.GetStimuli();
            var spikes = new double[][][] { DataLoader.GetSpikes()[0] };
            var deviation = new double[2][];

            var stopwatch = Stopwatch.StartNew();
            var staRounded = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, 0, RoundStrategy.Round);
            stopwatch.Stop();
            var values = String.Join(", ", Array.ConvertAll(staRounded, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (round strategy, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]");
            Console.WriteLine();

            stopwatch.Restart();
            var staCeiled = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, 0, RoundStrategy.Ceiling);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(staCeiled, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[0] = Analysis.Math.Subtract(staCeiled, staRounded);
            Console.WriteLine("STA (ceiling strategy, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]");
            Console.WriteLine();

            stopwatch.Restart();
            var staFloored = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, 0, RoundStrategy.Floor);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(staFloored, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[1] = Analysis.Math.Subtract(staFloored, staRounded);
            Console.WriteLine("STA (floor strategy, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]");
            Console.WriteLine();

            var averageDeviation = Analysis.Math.Mean(deviation);
            values = String.Join(", ", Array.ConvertAll(averageDeviation, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("Average Deviation from Round Strategy = \n[" + values + "]");
        }

        [TestMethod]
        public void TestCalculateSTA_PerCell()
        {
            Console.WriteLine("Test calculation of STA for each cell in spike data.\n");

            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            var stopwatch = Stopwatch.StartNew();
            var sta = SpikeTriggeredAnalysis.CalculateSTA(stimuli, new double[][][] { spikes[0] }, 0, RoundStrategy.Round);
            stopwatch.Stop();
            var values = String.Join(", ", Array.ConvertAll(sta, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (cell 1, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]\n");

            stopwatch.Restart();
            sta = SpikeTriggeredAnalysis.CalculateSTA(stimuli, new double[][][] { spikes[1] }, 0, RoundStrategy.Round);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(sta, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (cell 2, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]\n");

            stopwatch.Restart();
            sta = SpikeTriggeredAnalysis.CalculateSTA(stimuli, new double[][][] { spikes[2] }, 0, RoundStrategy.Round);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(sta, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (cell 3, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]\n");

            stopwatch.Restart();
            sta = SpikeTriggeredAnalysis.CalculateSTA(stimuli, new double[][][] { spikes[3] }, 0, RoundStrategy.Round);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(sta, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (cell 4, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]\n");
        }

        [TestMethod]
        public void TestCalculateSTC()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            var stopwatch = Stopwatch.StartNew();
            var stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, new double[][][] { spikes[0] }, RoundStrategy.Round);
            stopwatch.Stop();
            var values = string.Join("\n", Array.ConvertAll(stc, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nSTC (cell 1, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");

            stopwatch.Restart();
            stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, new double[][][] { spikes[1] }, RoundStrategy.Round);
            stopwatch.Stop();
            values = string.Join("\n", Array.ConvertAll(stc, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nSTC (cell 2, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");

            stopwatch.Restart();
            stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, new double[][][] { spikes[2] }, RoundStrategy.Round);
            stopwatch.Stop();
            values = string.Join("\n", Array.ConvertAll(stc, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nSTC (cell 3, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");

            stopwatch.Restart();
            stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, new double[][][] { spikes[3] }, RoundStrategy.Round);
            stopwatch.Stop();
            values = string.Join("\n", Array.ConvertAll(stc, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nSTC (cell 4, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");
        }

        [TestMethod]
        public void TestCalculateEigen()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            double[] eigenValues;
            double[][] eigenVectors;

            for (var cell = 0; cell < 4; cell++)
            {
                var stopwatch = Stopwatch.StartNew();

                var stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, new double[][][] { spikes[cell] }, RoundStrategy.Round);
                SpikeTriggeredAnalysis.CalculateEigenValues(stc, out eigenValues, out eigenVectors);

                stopwatch.Stop();

                var values = string.Join(", ", Array.ConvertAll(eigenValues, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
                Console.WriteLine("Eigen-values (cell " + cell.ToString() + ", duration: " + stopwatch.ElapsedMilliseconds + " ms) = [ " + values + " ]");
            }
        }

        [TestMethod]
        public void TestCalculateRF()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            var stopwatch = Stopwatch.StartNew();
            var receptiveField = SpikeTriggeredAnalysis.CalculateRF(stimuli, spikes, 16, 16, null);
            stopwatch.Stop();

            var values = string.Join("\n", Array.ConvertAll(receptiveField, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nReceptive Field (cell 1, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");
        }

        [TestMethod]
        public void TestCalculateRFSmoothed()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            var stopwatch = Stopwatch.StartNew();
            var kernel = (new Gaussian()).Kernel2D(3);
            var receptiveField = SpikeTriggeredAnalysis.CalculateRF(stimuli, spikes, 16, 16, kernel);
            stopwatch.Stop();

            var values = string.Join("\n", Array.ConvertAll(receptiveField, row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nReceptive Field (cell 1, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");
        }

        [TestMethod]
        public void TestMeanOfStimuli()
        {
            var stimuli = DataLoader.GetStimuli();

            var stopwatch = Stopwatch.StartNew();
            var mean = Math.Mean(stimuli);
            stopwatch.Stop();
            var values = String.Join(", ", Array.ConvertAll(mean, value => string.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", value)));
            Console.WriteLine("Mean(raw stimuli, duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[" + values + "]\n");
        }

        [TestMethod] 
        public void TestCalculateMatch()
        {
            //var stimuli = DataLoader.GetStimuli();
            //var spikes = DataLoader.GetSpikes();

            //var useDynamicDivisorForEdges = true;
            //var stopwatch = Stopwatch.StartNew();
            //var receptiveField = SpikeTriggeredAnalysis.CalculateRF(stimuli, spikes, 16, 16, null);
            //var smoothKernel = (new Gaussian()).Kernel2D(3);
            //var forSpikeTriggeredStimuliOnly = true;
            //var match = SpikeTriggeredAnalysis.CalculateSTAResponseHistogram(stimuli, spikes, forSpikeTriggeredStimuliOnly, 16, 16, smoothKernel, useDynamicDivisorForEdges, 10);

            //stopwatch.Stop();

            //var values = string.Join(", ", Array.ConvertAll(match, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            //Console.WriteLine("\nMatch Values (STA * Stimuli) (duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + values + "\n]");

            throw new NotImplementedException();
        }
    }
}
