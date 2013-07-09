using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Generic.Factorization;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class introduces some new extension method for types used in this project.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// This method returns the buckets of the histogram as an array.
        /// </summary>
        /// <param name="histogram">The histogram from which buckets are requested.</param>
        /// <returns>Returns an array with the bucket objects from the histogram.</returns>
        public static Bucket[] Buckets(this Histogram histogram)
        {
            var result = new Bucket[histogram.BucketCount];

            for (int index = 0; index < histogram.BucketCount; index++)
                result[index] = histogram[index];

            return result;
        }

        /// <summary>
        /// This method calculates the mean (center of distribution; expected value of random variable X) of an histogram.
        /// This is a weighted average of the class marks, with the relative frequencies as the weight factors.
        /// </summary>
        /// <param name="histogram">The histogram for which the mean will be calculated.</param>
        /// <returns>Returns the mean value of the histogram as a single floating point value.</returns>
        public static double Mean(this Histogram histogram)
        {
            var result = 0d;

            for (var index = 0; index < histogram.BucketCount; index++)
            {
                var bucket = histogram[index];
                result += index * (bucket.Count / histogram.DataCount);
            }

            result /= histogram.DataCount;

            return result;
        }

        /// <summary>
        /// This method calculates the variance of a data set (probability distribution). That is the arithmetic average 
        /// of the squared differences between data values and the mean.
        /// </summary>
        /// <param name="histogram">The histogram for which variance will be calculated.</param>
        /// <returns>Returns the variance of the histogram as a single floating point value.</returns>
        public static double Variance(this Histogram histogram)
        {
            var data = new double[histogram.BucketCount];

            for (var index = 0; index < histogram.BucketCount; index++)
                data[index] = (histogram[index].Count / histogram.DataCount);

            var result = Math.Variance(data);

            return result;
        }

        // TODO: [RS] Documentation
        public static double RelativeCount(this Bucket bucket, Histogram histogram)
        {
            if (histogram.DataCount == 0)
                return 0;

            return bucket.Count / histogram.DataCount;
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
