using System;
using System.Diagnostics;
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

            var stopwatch = Stopwatch.StartNew();
            var staRounded = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, RoundStrategy.Round);
            stopwatch.Stop();
            var values = String.Join(", ", Array.ConvertAll(staRounded, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("STA (round strategy) = [" + values + "]");
            Console.WriteLine("Duration STA calculation: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine();

            stopwatch.Restart();
            var staCeiled = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, RoundStrategy.Ceiling);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(staCeiled, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[0] = Analysis.Math.Subtract(staCeiled, staRounded);
            Console.WriteLine("STA (ceiling strategy) = [" + values + "]");
            Console.WriteLine("Duration STA calculation: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine();

            stopwatch.Restart();
            var staFloored = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, RoundStrategy.Floor);
            stopwatch.Stop();
            values = String.Join(", ", Array.ConvertAll(staFloored, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            deviation[1] = Analysis.Math.Subtract(staFloored, staRounded);
            Console.WriteLine("STA (floor strategy) = [" + values + "]");
            Console.WriteLine("Duration STA calculation: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine();

            var averageDeviation = Analysis.Math.Mean(deviation);
            values = String.Join(", ", Array.ConvertAll(averageDeviation, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)));
            Console.WriteLine("Average Deviation from Round Strategy = [" + values + "]");
        }

        [TestMethod]
        public void TestCalculateSTC()
        {
            var stimuli = DataLoader.GetStimuli();
            var spikes = DataLoader.GetSpikes();

            var stopwatch = Stopwatch.StartNew();
            var stc = SpikeTriggeredAnalysis.CalculateSTC(stimuli, spikes, RoundStrategy.Round);
            stopwatch.Stop();

            var values = Array.ConvertAll(stc,
                                          row =>
                                          Array.ConvertAll(row,
                                                           value =>
                                                           string.Format(CultureInfo.InvariantCulture, "{0:0.0000}",
                                                                         value)));

            
            Console.WriteLine("\nSTC = [");
            foreach (var row in values)
            {
                foreach (var value in row)
                    Console.Write(value + "\t");

                Console.WriteLine();
            }
            Console.Write("]");

            Console.WriteLine();
            Console.WriteLine("\nDuration STC calculation: " + stopwatch.ElapsedMilliseconds + " ms");
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
        public void TestDivide2()
        {
            var matrix = new double[][]
                {
                    new double[] {1.1, 1.2, 1.7, 1.8},
                    new double[] {2.1, 2.2, 2.7, 2.8},
                    new double[] {6.1, 6.2, 6.7, 6.8}
                };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix,
                                           elementA =>
                                           Array.ConvertAll(elementA,
                                                            elementB =>
                                                            string.Format(CultureInfo.InvariantCulture, "{0:0.0000}",
                                                                          elementB)));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

                Console.WriteLine();
            }

            Console.WriteLine("Divisor: 2");
            Console.WriteLine();

            Console.WriteLine("Divided:");
            var result = Analysis.Math.Divide(matrix, 2);
            strings = Array.ConvertAll(result,
                                       elementA =>
                                       Array.ConvertAll(elementA,
                                                        elementB =>
                                                        string.Format(CultureInfo.InvariantCulture,
                                                                      "{0:0.0000}", elementB)));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

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

        [TestMethod]
        public void TestSubtract()
        {
            var matrix = new double[][] 
            { 
                new double[] { 1.1, 1.2, 1.7, 1.8 }, 
                new double[] { 2.1, 2.2, 2.7, 2.8 }, 
                new double[] { 6.1, 6.2, 6.7, 6.8 } 
            };

            Console.WriteLine("Original:");
            var strings = Array.ConvertAll(matrix,
                                           elementA =>
                                           Array.ConvertAll(elementA,
                                                            elementB =>
                                                            string.Format(CultureInfo.InvariantCulture, "{0:0.0000}",
                                                                          elementB)));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

                Console.WriteLine();
            }

            var vector = new double[] {2.5, 1.5, -2.5, -1.5};
            Console.WriteLine("Vector: (2.5, 1.5, -2.5, -1.5)");
            Console.WriteLine();

            Console.WriteLine("Subtracted:");
            var result = Analysis.Math.Subtract(matrix, vector);
            strings = Array.ConvertAll(result,
                                       elementA =>
                                       Array.ConvertAll(elementA,
                                                        elementB => string.Format(CultureInfo.InvariantCulture,
                                                                                  "{0:0.0000}", elementB)));

            foreach (var itemA in strings)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestSum()
        {
            var matrices = new double[][][]
                {
                    new double[][]
                        {
                            new double[] {1.1, 1.2, 1.7, 1.8},
                            new double[] {2.1, 2.2, 2.7, 2.8},
                            new double[] {6.1, 6.2, 6.7, 6.8}
                        },

                    new double[][]
                        {
                            new double[] {-1.1, -1.2, -1.7, -1.8},
                            new double[] {-2.1, -2.2, -2.7, -2.8},
                            new double[] {-6.1, -6.2, -6.7, -6.8}
                        },

                    new double[][]
                        {
                            new double[] {1.1, 2.1, 3.1, 4.1},
                            new double[] {1.2, 2.2, 3.2, 4.2},
                            new double[] {1.3, 2.3, 3.3, 4.3}
                        }
                };

            Console.WriteLine("Original:");
            var stringsMatrices = Array.ConvertAll(matrices, matrix =>
                                                     Array.ConvertAll(matrix,
                                                                      elementA =>
                                                                      Array.ConvertAll(elementA,
                                                                                       elementB =>
                                                                                       string.Format(
                                                                                           CultureInfo.InvariantCulture,
                                                                                           "{0:0.0000}",
                                                                                           elementB))));

            foreach (var matrix in stringsMatrices)
            {
                foreach (var itemA in matrix)
                {
                    foreach (var itemB in itemA)
                        Console.Write(itemB + "\t");

                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            Console.WriteLine("Sum:");
            var result = Analysis.Math.Sum(matrices);

            var stringsMatrix = Array.ConvertAll(result,
                                                 elementA =>
                                                 Array.ConvertAll(elementA,
                                                                  elementB =>
                                                                  string.Format(CultureInfo.InvariantCulture,
                                                                                "{0:0.0000}", elementB)));

            foreach (var itemA in stringsMatrix)
            {
                foreach (var itemB in itemA)
                    Console.Write(itemB + "\t");

                Console.WriteLine();
            }
        }
    }
}
