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
        public void TestCalculateSTA()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();
            var deviation = new double[2][];

            var staRounded = STA.Calculate(stimuli, spikes, RoundStrategy.Round);
            var values = String.Join(", ", Array.ConvertAll(staRounded, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (round strategy) = [" + values + "]");
            Console.WriteLine();

            var staCeiled = STA.Calculate(stimuli, spikes, RoundStrategy.Ceiling);
            values = String.Join(", ", Array.ConvertAll(staCeiled, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[0] = Analysis.Math.Subtract(staRounded, staCeiled);
            Console.WriteLine("STA (ceiling strategy) = [" + values + "]");
            Console.WriteLine();

            var staFloored = STA.Calculate(stimuli, spikes, RoundStrategy.Floor);
            values = String.Join(", ", Array.ConvertAll(staFloored, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[1] = Analysis.Math.Subtract(staRounded, staFloored);
            Console.WriteLine("STA (floor strategy) = [" + values + "]");
            Console.WriteLine();

            var averageDeviation = Analysis.Math.Mean(deviation);
            values = String.Join(", ", Array.ConvertAll(averageDeviation, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("Average Deviation from Round Strategy = [" + values + "]");
            
        }

        [TestMethod]
        public void TestFloor()
        {
            var matrix = new double[][][] 
            { 
                new double[][] { new double[] { 1.1 }, new double[] { 1.2 }, new double[] { 1.7 }, new double[] { 1.8 } }, 
                new double[][] { new double[] { 2.1 }, new double[] { 2.2 }, new double[] { 2.7 }, new double[] { 2.8 } }, 
                new double[][] { new double[] { 6.1 }, new double[] { 6.2 }, new double[] { 6.7 }, new double[] { 6.8 } } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));
            
            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Floored:");
            var result = Analysis.Math.Floor(matrix);
            strings = Array.ConvertAll(result, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestCeiling()
        {
            var matrix = new double[][][] 
            { 
                new double[][] { new double[] { 1.1 }, new double[] { 1.2 }, new double[] { 1.7 }, new double[] { 1.8 } }, 
                new double[][] { new double[] { 2.1 }, new double[] { 2.2 }, new double[] { 2.7 }, new double[] { 2.8 } }, 
                new double[][] { new double[] { 6.1 }, new double[] { 6.2 }, new double[] { 6.7 }, new double[] { 6.8 } } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Ceiled:");
            var result = Analysis.Math.Ceiling(matrix);
            strings = Array.ConvertAll(result, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestRound()
        {
            var matrix = new double[][][] 
            { 
                new double[][] { new double[] { 1.1 }, new double[] { 1.2 }, new double[] { 1.7 }, new double[] { 1.8 } }, 
                new double[][] { new double[] { 2.1 }, new double[] { 2.2 }, new double[] { 2.7 }, new double[] { 2.8 } }, 
                new double[][] { new double[] { 6.1 }, new double[] { 6.2 }, new double[] { 6.7 }, new double[] { 6.8 } } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Rounded:");
            var result = Analysis.Math.Round(matrix);
            strings = Array.ConvertAll(result, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestDivide()
        {
            var matrix = new double[][][] 
            { 
                new double[][] { new double[] { 1.1 }, new double[] { 1.2 }, new double[] { 1.7 }, new double[] { 1.8 } }, 
                new double[][] { new double[] { 2.1 }, new double[] { 2.2 }, new double[] { 2.7 }, new double[] { 2.8 } }, 
                new double[][] { new double[] { 6.1 }, new double[] { 6.2 }, new double[] { 6.7 }, new double[] { 6.8 } } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Divisor: 2");
            Console.WriteLine();

            Console.WriteLine("Divided:");
            var result = Analysis.Math.Divide(matrix, 2);
            strings = Array.ConvertAll(result, elementA => Array.ConvertAll(elementA, elementB => Array.ConvertAll(elementB, elementC => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementC))));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                {
                    foreach (var itemC in itemB)
                        Console.Write(itemC + "\t");
                }

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestMean()
        {
            var matrix = new double[][] 
            { 
                new double[] { 1.1, 1.2, 1.7, 1.8 }, 
                new double[] { 2.1, 2.2, 2.7, 2.8 }, 
                new double[] { 6.1, 6.2, 6.7, 6.8 } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix, elementA => Array.ConvertAll(elementA, elementB => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementB)));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Mean:");
            var result = Analysis.Math.Mean(matrix);
            var strings2 = Array.ConvertAll(result, elementA => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", elementA));

            foreach (var item in strings2)
                Console.Write(item + "\t");
        }

    }
}
