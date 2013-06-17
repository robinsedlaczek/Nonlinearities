﻿using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Generic.Factorization;
using MathNet.Numerics.Statistics;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class introduces some new extension method for types used in this project.
    /// </summary>
    public static class Extensions
    {
        public static double Mean(this Histogram histogram)
        {
            var result = 0d;

            for (var index = 0; index < histogram.BucketCount; index++)
            {
                var bucket = histogram[index];
                result += index * bucket.Count;
            }

            result /= histogram.DataCount;

            return result;
        }

        /// <summary>
        /// This method returns the values of a matrix as an array of arrays rather than a multi-dimensional array.
        /// The values will be copied element by element. So the result is a real copy of the original matrix. 
        /// </summary>
        /// <param name="matrix">The matrix to copy into an array.</param>
        /// <returns>
        /// Returns an array of arrays where the first dimension contains the rows and the second dimension contains 
        /// the column values of the original matrix.
        /// </returns>
        public static double[][] AsArray(this Matrix<double> matrix)
        {
            var result = new double[matrix.RowCount][];

            for (var row = 0; row < matrix.RowCount; row++)
            {
                result[row] = new double[matrix.ColumnCount];

                for (var column = 0; column < matrix.ColumnCount; column++)
                    result[row][column] = matrix[row, column];
            }

            return result;
        }

        /// <summary>
        /// This method creates an DenseMatrix object from a matrix given as an array of arrays.
        /// </summary>
        /// <param name="matrix">An arbitrary matrix used to create the new matrix.</param>
        /// <param name="array">
        /// The array containing the matrix values where the first dimension contains the rows and the second dimension 
        /// contains the columns of the matrix.
        /// </param>
        /// <returns>Returns the new dense matrix given by the array.</returns>
        public static DenseMatrix OfArray(this DenseMatrix matrix, double[][] array)
        {
            var rows = array.Length;
            var columns = array[0].Length;
            var tempMatrix = new double[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                    tempMatrix[row, column] = array[row][column];
            }

            return DenseMatrix.OfArray(tempMatrix);
        }
    }
}
