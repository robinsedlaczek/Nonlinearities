using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Generic.Factorization;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class introduces some new extension method for types used in this project.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// This method returns the Eigenvectors of a matrix as an array of arrays rather than a multi-dimensional array (C#-stuff).
        /// </summary>
        /// <param name="evd">The Eigen value decomposition of the matrix.</param>
        /// <returns>
        /// Returns an array of arrays where the first dimension contains the vectors and the second dimension contains the values of each vector.
        /// </returns>
        public static double[][] EigenVectorsAsArrayOfArray(this Evd<double> evd)
        {
            Matrix<double> evdEigenVectors = evd.EigenVectors();

            var eigenVectors = new double[evdEigenVectors.RowCount][];

            for (var vector = 0; vector < evdEigenVectors.RowCount; vector++)
            {
                for (var value = 0; value < evdEigenVectors.ColumnCount; value++)
                {
                    eigenVectors[vector] = new double[evdEigenVectors.ColumnCount];
                    eigenVectors[vector][value] = evdEigenVectors[vector, value];
                }
            }

            return eigenVectors;
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
