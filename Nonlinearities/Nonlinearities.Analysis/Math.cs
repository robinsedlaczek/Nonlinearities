﻿using System.Linq;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class provides services for matrix and vector calculations.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// This method creates a copy of a matrix.
        /// </summary>
        /// <param name="source">The matrix to copy.</param>
        /// <returns>Returns the copy of the matrix.</returns>
        public static double[][][] Copy(double[][][] source)
        {
            var result = new double[source.Length][][];

            for (var indexA = 0; indexA < source.Length; indexA++ )
            {
                result[indexA] = new double[source[indexA].Length][];

                for (var indexB = 0; indexB < source[indexA].Length; indexB++)
                {
                    result[indexA][indexB] = new double[source[indexA][indexB].Length];

                    for (var indexC=0; indexC< source[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = source[indexA][indexB][indexC];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method devides a matrix by a scalar value.
        /// </summary>
        /// <param name="devident">The matrix to divide.</param>
        /// <param name="divisor">The scalar by which will be divided.</param>
        /// <returns>Returns the resulting matrix.</returns>
        public static double[][][] Divide(double[][][] devident, double divisor)
        {
            if (divisor < 0.00000001)
                throw new DivideByZeroException("The devisor must not equal 0.");

            var stopwatch = Stopwatch.StartNew();
            var result = Copy(devident);

            for (var indexA = 0; indexA < devident.Length; indexA++)
            {
                for (var indexB = 0; indexB < devident[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < devident[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = devident[indexA][indexB][indexC] / divisor;
                    }
                }
            }

            stopwatch.Stop();
            // Console.WriteLine("Duration Divide(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        /// <summary>
        /// This method devides a matrix by a scalar value. Division is done value-by-value.
        /// </summary>
        /// <param name="devident">The matrix to divide.</param>
        /// <param name="divisor">The scalar by which will be divided.</param>
        /// <returns>Returns the resulting matrix.</returns>
        public static double[][] Divide(double[][] devident, double divisor)
        {
            if (divisor < 0.00000001)
                throw new DivideByZeroException("The devisor must not equal 0.");

            var stopwatch = Stopwatch.StartNew();
            var result = new double[devident.Length][];

            for (var y = 0; y < devident.Length; y++)
            {
                result[y] = new double[devident[y].Length];

                for (var x = 0; x < devident[y].Length; x++)
                    result[y][x] = devident[y][x]/divisor;
            }

            stopwatch.Stop();
            // Console.WriteLine("Duration Divide(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        /// <summary>
        /// This method rounds the values of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to round.</param>
        /// <returns>Returns the resulting matrix with rounded values.</returns>
        public static double[][][] Round(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = Copy(matrix);

            for (var indexA = 0; indexA < matrix.Length; indexA++)
            {
                for (var indexB = 0; indexB < matrix[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < matrix[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = System.Math.Round(matrix[indexA][indexB][indexC]);
                    }
                }
            }

            stopwatch.Stop();
            // Console.WriteLine("Duration Round(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        /// <summary>
        /// This method reduces the values of a matrix to the biggest integer value smaller than the value self.
        /// </summary>
        /// <param name="matrix">The matrix to floor.</param>
        /// <returns>Returns the resulting matrix.</returns>
        public static double[][][] Floor(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = Copy(matrix);

            for (var indexA = 0; indexA < matrix.Length; indexA++)
            {
                for (var indexB = 0; indexB < matrix[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < matrix[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = System.Math.Floor(matrix[indexA][indexB][indexC]);
                    }
                }
            }

            stopwatch.Stop();
            // Console.WriteLine("Duration Floor(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        /// <summary>
        /// This method increases the values of a matrix to the smallest integer value bigger than the value self.
        /// </summary>
        /// <param name="matrix">The matrix to ceil.</param>
        /// <returns>Returns the resulting matrix.</returns>
        public static double[][][] Ceiling(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = Copy(matrix);

            for (var indexA = 0; indexA < matrix.Length; indexA++)
            {
                for (var indexB = 0; indexB < matrix[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < matrix[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = System.Math.Ceiling(matrix[indexA][indexB][indexC]);
                    }
                }
            }

            stopwatch.Stop();
            // Console.WriteLine("Duration Ceiling(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        /// <summary>
        /// This method calculates the mean (average) of a matrix. The average value will be calculated 
        /// per column of the matrix. A horizontal vector results.
        /// </summary>
        /// <param name="matrix">The matrix to average.</param>
        /// <returns>Returns a horizontal vector containing the average of each column of the matrix.</returns>
        public static double[] Mean(double[][] matrix)
        {
            var values = new List<double>();
            
            foreach (var row in matrix)
            {
                for (var index = 0; index < row.Length; index++)
                {
                    if (values.Count == index)
                        values.Add(row[index]);
                    else
                        values[index] += row[index];
                }
            }

            for (var index = 0; index < values.Count; index++)
                values[index] /= matrix.Length;

            return values.ToArray();
        }

        /// <summary>
        /// This method calculates the variance of a data set (probability distribution). That is the arithmetic average 
        /// of the squared differences between data values and the mean.
        /// </summary>
        /// <param name="data">The original data values.</param>
        /// <returns>Returns the variance of the data set as a single floating point value.</returns>
        public static double Variance(double[] data)
        {
            var mean = data.Average();
            var squaredDifferencesSum = 0d;

            for (var index = 0; index < data.Length; index++)
                squaredDifferencesSum += System.Math.Pow(data[index] - mean, 2);

            var result = squaredDifferencesSum / data.Length;

            return result;
        }

        /// <summary>
        /// This method calculates the standard deviation of a data set (probability distribution). That is the square 
        /// root of the variance of the data set.
        /// </summary>
        /// <param name="originalData">The original data values.</param>
        /// <returns>Returns the standard deviation of the data set as a single floating point value.</returns>
        public static double StandardDeviation(double[] originalData)
        {
            var variance = Variance( originalData);
            var result = System.Math.Sqrt(variance);

            return result;
        }

        /// <summary>
        /// This method calculates the tensor (outer) product of two vectors.
        /// </summary>
        /// <param name="horizontalVector">The first vector of the operation.</param>
        /// <param name="verticalVector">The second vector of the operation.</param>
        /// <returns>Returns a matrix representing the tensor (outer) product of both vectors.</returns>
        public static double[][] Tensor(double[] horizontalVector, double[] verticalVector)
        {
            var result = new double[verticalVector.Length][];
            
            for (var y = 0; y < verticalVector.Length; y++ )
            {
                result[y] = new double[horizontalVector.Length];

                for (var x = 0; x < horizontalVector.Length; x++)
                {
                    result[y][x] = verticalVector[y] * horizontalVector[x];
                }
            }

            return result;
        }

        /// <summary>
        /// This method subtracts two vectors value-by-value.
        /// </summary>
        /// <param name="a">Vector which will be reduced.</param>
        /// <param name="b">Vector which reduces the first vector.</param>
        /// <returns>Returns the resulting vector.</returns>
        public static double[] Subtract(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("a and b must be arrays of equal length.");

            var result = new double[a.Length];

            for (var index = 0; index < a.Length; index++)
                result[index] = a[index] - b[index];

            return result;
        }

        /// <summary>
        /// This method subtracts a vector from each vector in a list.
        /// </summary>
        /// <param name="vectorList">List of vectors which will be reduced.</param>
        /// <param name="vector">The vector which will reduce the list of vectors.</param>
        /// <returns>Returns the original list of vectors where all vectors in it are reduced.</returns>
        public static double[][] Subtract(double[][] vectorList, double[] vector)
        {
            var result = new double[vectorList.Length][];

            for (var vectorIndex =0; vectorIndex<vectorList.Length; vectorIndex++)
            {
                result[vectorIndex] = new double[vectorList[vectorIndex].Length];

                for (var valueIndex = 0; valueIndex < vectorList[vectorIndex].Length; valueIndex++)
                    result[vectorIndex][valueIndex] = vectorList[vectorIndex][valueIndex] - vector[valueIndex];                
            }

            return result;
        }

        /// <summary>
        /// This method subtracts a scalar from each value in an abritary 3-by-3 matrix.
        /// </summary>
        /// <param name="matrix">The 3-by-3 matrix where to reduce the values.</param>
        /// <param name="scalar">The scalar which will be subtracted from each value in the matrix.</param>
        /// <returns>Returns the resulting matrix where all values are reduced by the scalar.</returns>
        public static double[][][] Subtract(double[][][] matrix, double scalar)
        {
            var result = Copy(matrix);

            for (var indexA = 0; indexA < matrix.Length; indexA++)
            {
                for (var indexB = 0; indexB < matrix[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < matrix[indexA][indexB].Length; indexC++)
                    {
                        result[indexA][indexB][indexC] = matrix[indexA][indexB][indexC] - scalar;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method calculates the sum (value-by-value) of a list of matrices.
        /// </summary>
        /// <param name="matrices">The list of matrices that will be summed.</param>
        /// <returns>Returns the resulting matrix representing the sum of all other matrices in the list.</returns>
        public static double[][] Sum(double[][][] matrices)
        {
            double[][] result = null;

            foreach (var matrix in matrices)
            {
                if (result == null)
                    result = new double[matrix.Length][];

                for (int y = 0; y < matrix.Length; y++)
                {
                    if (result[y] == null)
                        result[y] = new double[matrix[y].Length];

                    for (int x = 0; x < matrix[y].Length; x++)
                        result[y][x] += matrix[y][x];
                }
            }

            return result;
        }

        /// <summary>
        /// This method finds the minimum and maximum value in a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to search in.</param>
        /// <param name="max">The output parameter for the maximum value.</param>
        /// <param name="min">The output parameter for the minimum value.</param>
        /// <returns>Returns the smallest and the biggest value in the matrix.</returns>
        public static void MinMax(double[][] matrix, out double min, out double max)
        {
            min = matrix[0][0];
            max = matrix[0][0];

            foreach (var row in matrix)
            {
                foreach (var value in row)
                {
                    if (value < min)
                        min = value;

                    if (value > max)
                        max = value;
                }
            }
        }

        /// <summary>
        /// This service calculates the convolution of a matrix and a kernel.
        /// </summary>
        /// <param name="source">The source matrix on which the kernel is applied via convolution operation.</param>
        /// <param name="sourceRows">The number of rows of the source matrix.</param>
        /// <param name="sourceColumns">The number of columns of the source matrix.</param>
        /// <param name="kernelMatrix">The kernel matrix which will be applied to the source matrix via convolution operation.</param>
        /// <param name="useDynamicDivisorForEdges">
        /// Indicates whether to devide the resulting matrix value by the real number of neighbors processed in one iteration (e.g. there are
        /// less neighbors at the edge of the matrix) or by the number of all possible neighbors.
        /// </param>
        /// <returns>Returns the matrix resulting from the convolution operation.</returns>
        public static double[][] Convolution(double[][] source, int sourceRows, int sourceColumns, double[,] kernelMatrix, bool useDynamicDivisorForEdges = true)
        {
            var result = new double[sourceRows][];

            var kernelRows = kernelMatrix.GetLength(0);
            var kernelColumns = kernelMatrix.GetLength(1);

            // check kernel size
            if ((kernelRows != kernelColumns) || (kernelRows < 3) || (kernelRows > 99) || (kernelRows%2 == 0))
                throw new ArgumentException("Invalid kernel size.");

            // kernel size
            var kernelSize = kernelRows ^ 2;

            // calculate divisor
            var divisor = 0d;
            for (int row = 0, n = kernelRows; row < n; row++)
            {
                for (int column = 0, kernelColumn = kernelColumns; column < kernelColumns; column++)
                    divisor += kernelMatrix[row, column];
            }

            if (System.Math.Abs(divisor - 0d) < double.Epsilon)
                divisor = 1;

            // loop and array indexes
            int i, j, ir, jr;
            double k;

            // kernel's radius
            var radius = kernelRows >> 1;

            // color sums
            double g;
            double div;

            // number of kernel elements taken into account
            int processedKernelSize;

            // for each line
            for (int y = 0; y < sourceRows; y++)
            {
                result[y] = new double[sourceColumns];

                // for each pixel
                for (int x = 0; x < sourceColumns; x++)
                {
                    g = div = processedKernelSize = 0;

                    // for each kernel row
                    for (i = 0; i < kernelRows; i++)
                    {
                        ir = i - radius;
                        var ty = y + ir;

                        // skip row
                        if (ty < 0)
                            continue;

                        // break
                        if (ty >= sourceRows)
                            break;

                        // for each kernel column
                        for (j = 0; j < kernelColumns; j++)
                        {
                            jr = j - radius;
                            var tx = x + jr;

                            // skip column
                            if ((tx < 0) || (tx >= sourceColumns))
                                continue;

                            k = kernelMatrix[i, j];

                            div += k;
                            g += k * source[ty][tx];
                            processedKernelSize++;
                        }
                    }

                    // check if all kernel elements were processed
                    if (processedKernelSize == kernelSize)
                    {
                        // all kernel elements are processed - we are not on the edge
                        div = divisor;
                    }
                    else
                    {
                        // we are on edge. do we need to use dynamic divisor or not?
                        if (!useDynamicDivisorForEdges)
                            div = divisor;
                    }

                    // check divider
                    if (System.Math.Abs(div - 0) > double.Epsilon)
                        g /= div;

                    result[y][x] = g;
                }
            }

            return result;
        }

        // TODO: Document!
        public static Histogram CalculateHistogram(double[] values, int buckets)
        {
            return new Histogram(values, buckets);
        }

    }
}
