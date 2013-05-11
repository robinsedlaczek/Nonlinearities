using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonlinearities.Analysis
{
    public static class Math
    {
        public static double[][][] Divide(double[][][] devident, double divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("The devisor must not equal 0.");

            var stopwatch = Stopwatch.StartNew();
            var result = devident.Clone() as double[][][];

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
            Console.WriteLine("Duration Divide(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        public static double[][][] Round(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = matrix.Clone() as double[][][];

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
            Console.WriteLine("Duration Round(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        public static double[][][] Floor(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = matrix.Clone() as double[][][];

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
            Console.WriteLine("Duration Floor(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        public static double[][][] Ceiling(double[][][] matrix)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = matrix.Clone() as double[][][];

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
            Console.WriteLine("Duration Ceiling(): " + stopwatch.ElapsedMilliseconds + " ms");

            return result;
        }

        public static double[] Subtract(double[] a, double[] b)
        {
            if (a.Length!= b.Length)
                throw new ArgumentException("a and b must be arrays of equal length.");

            var result = new double[a.Length];

            for (var index = 0; index < a.Length; index++)
                result[index] = a[index] - b[index];

            return result;
        }

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
    }
}
