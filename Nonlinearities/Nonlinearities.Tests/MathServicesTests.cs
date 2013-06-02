using System;
using System.Diagnostics;
using System.Globalization;
using AForge.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonlinearities.Analysis;

namespace Nonlinearities.Tests
{
    [TestClass]
    public class MathServicesTests
    {
        [TestMethod]
        public void TestFloor()
        {
            var matrix = new double[][][]
                {
                    new double[][] { new[] { 1.1 }, new[] { 1.2 }, new[] { 1.7 }, new[] { 1.8 } },
                    new double[][] { new[] { 2.1 }, new[] { 2.2 }, new[] { 2.7 }, new[] { 2.8 } },
                    new double[][]  { new[] { 6.1 }, new[] { 6.2 }, new[] { 6.7 }, new[] { 6.8 } }
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

            var vector = new double[] { 2.5, 1.5, -2.5, -1.5 };
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

        [TestMethod]
        public void TestConvolutionPreserveOrigianl()
        {
            var source = new double[][]
                {
                    new double[] { 1, 0, 0, 0, 1 },
                    new double[] { 0, 1, 0, 1, 0 },
                    new double[] { 0, 0, 1, 0, 0 },
                    new double[] { 0, 1, 0, 1, 0 },
                    new double[] { 1, 0, 0, 0, 1 }
                };

            var kernel = new double[3,3]
                {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 0, 0 }
                };
            
            const bool useDynamicDivisorForEdges = true;
            const int threshold = 0;

            var stopwatch = Stopwatch.StartNew();
            var convolution = Analysis.Math.Convolution(source, source.Length, source[0].Length, kernel, useDynamicDivisorForEdges);
            stopwatch.Stop();

            var sourceValues = string.Join("\n", Array.ConvertAll(source, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0}", value)))));
            Console.WriteLine("\nSource = \n[\n" + sourceValues + "\n]");

            // var kernelValues = string.Join("\n", Array.ConvertAll((double[,])kernelOriginal , row => string.Join("\t", Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", value)))));
            Console.WriteLine("\nKernel (original) = \n" + 
                              "[\n" +
                              "   0   0   0\n" +
                              "   0   1   0\n" +
                              "   0   0   0\n" + 
                              "]");

            var resultValues = string.Join("\n", Array.ConvertAll(convolution, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0}", value)))));
            Console.WriteLine("\nConvolution (duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + resultValues + "\n]");
        }

        [TestMethod]
        public void TestConvolutionAllNeighbors()
        {
            var source = new double[][]
                {
                    new double[] { 1, 0, 0, 0, 1 },
                    new double[] { 0, 1, 0, 1, 0 },
                    new double[] { 0, 0, 1, 0, 0 },
                    new double[] { 0, 1, 0, 1, 0 },
                    new double[] { 1, 0, 0, 0, 1 }
                };

            var kernel = new double[3, 3]
                {
                    { 1, 1, 1 },
                    { 1, 1, 1 },
                    { 1, 1, 1 }
                };

            const bool useDynamicDivisorForEdges = false;
            const int threshold = 0;

            var stopwatch = Stopwatch.StartNew();
            var convolution = Analysis.Math.Convolution(source, source.Length, source[0].Length, kernel, useDynamicDivisorForEdges);
            stopwatch.Stop();

            var sourceValues = string.Join("\n", Array.ConvertAll(source, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0}", value)))));
            Console.WriteLine("\nSource = \n[\n" + sourceValues + "\n]");

            Console.WriteLine("\nKernel (all neighbors) = \n" +
                              "[\n" +
                              "   1   1   1\n" +
                              "   1   1   1\n" +
                              "   1   1   1\n" +
                              "]");

            var resultValues = string.Join("\n", Array.ConvertAll(convolution, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0.00}", value)))));
            Console.WriteLine("\nConvolution (duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + resultValues + "\n]");
        }

        [TestMethod]
        public void TestConvolutionGaussian()
        {
            var source = new double[][]
                {
                    new double[] { 0, 0, 0, 0, 0 },
                    new double[] { 0, 0, 0, 0, 0 },
                    new double[] { 0, 0, 1, 0, 0 },
                    new double[] { 0, 0, 0, 0, 0 },
                    new double[] { 0, 0, 0, 0, 0 }
                };

            var gaussian = new Gaussian();
            var kernel = gaussian.Kernel2D(3);

            const bool useDynamicDivisorForEdges = false;

            var stopwatch = Stopwatch.StartNew();
            var convolution = Analysis.Math.Convolution(source, source.Length, source[0].Length, kernel, useDynamicDivisorForEdges);
            stopwatch.Stop();

            var sourceValues = string.Join("\n", Array.ConvertAll(source, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0}", value)))));
            Console.WriteLine("\nSource = \n[\n" + sourceValues + "\n]");

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "\nKernel (Gaussian) = \n" +
                    "[\n" +
                    "   {0:0.000}   {1:0.000}   {2:0.000}\n" +
                    "   {3:0.000}   {4:0.000}   {5:0.000}\n" +
                    "   {6:0.000}   {7:0.000}   {8:0.000}\n" +
                    "]",
                    kernel[0, 0], kernel[0, 1], kernel[0, 2],
                    kernel[1, 0], kernel[1, 1], kernel[1, 2],
                    kernel[2, 0], kernel[2, 1], kernel[2, 2]));

            var resultValues = string.Join("\n", Array.ConvertAll(convolution, row => string.Join(string.Empty, Array.ConvertAll(row, value => string.Format(CultureInfo.InvariantCulture, "   {0:0.00}", value)))));
            Console.WriteLine("\nConvolution (duration: " + stopwatch.ElapsedMilliseconds + " ms) = \n[\n" + resultValues + "\n]");
        }
    }
}
