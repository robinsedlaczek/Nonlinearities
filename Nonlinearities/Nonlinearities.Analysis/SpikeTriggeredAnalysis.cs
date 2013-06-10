using System;
using System.Collections.Generic;
using System.Linq;
using AForge.Math;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class provides services for Spike-triggered Analysis.
    /// </summary>
    public static class SpikeTriggeredAnalysis
    {
        #region Public Members

        /// <summary>
        /// This service calculates the spike-triggered average for given stimuli data and response spikes data.
        /// </summary>
        /// <param name="stimuli">
        /// The stimuli that triggered spikes as 2-dimensional array with:
        /// [?][ ] - List of stimuli frames.
        /// [ ][?] - Stimulus data for the frame.</param>
        /// <param name="spikes">
        /// The detected spikes for the stimuli as 2-dimensional array with:
        /// [?][ ][ ] - List of cells.
        /// [ ][?][ ] - List of spike data for the cell.
        /// [ ][ ][?] - Time when spike occurred.
        /// </param>
        /// <param name="frameOffset">
        /// The offset of the first frame which is taken for calculations. All other frames will be behind that frame.
        /// </param>
        /// <param name="roundStrategy">
        /// The strategy for rounding the frame numbers.
        /// </param>
        /// <returns>Returns a vector containing the STA. </returns>
        /// <remarks>
        /// Calculation of STA:
        /// 
        ///      N
        /// A =  Σ  s(tn)
        ///     n=1
        ///     ---------
        ///         N
        /// 
        /// N  = total number of spikes
        /// s  = vector representing the stimuli
        /// tn = time when the nth spike occurred
        /// A  = resulting STA (spike-triggered average)
        /// </remarks>
        public static double[] CalculateSTA(double[][] stimuli, double[][][] spikes, int frameOffset, RoundStrategy roundStrategy)
        {
            const double frameInterval = 1 / 59.721395; // = 0.016744 ms
            var spikeTriggeredStimulusEnsemble = GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, frameOffset, roundStrategy);

            var sta = Math.Subtract(Math.Mean(spikeTriggeredStimulusEnsemble), Math.Mean(stimuli));

            return sta;
        }

        /// <summary>
        /// This service calculates the receptive field for given stimuli data and response spikes data.
        /// </summary>
        /// <param name="stimuli">
        /// The stimuli that triggered spikes as 2-dimensional array with:
        /// [?][ ] - List of stimuli frames.
        /// [ ][?] - Stimulus data for the frame.</param>
        /// <param name="spikes">
        /// The detected spikes for the stimuli as 2-dimensional array with:
        /// [?][ ][ ] - List of cells.
        /// [ ][?][ ] - List of spike data for the cell.
        /// [ ][ ][?] - Time when spike occurred.
        /// </param>
        /// <param name="offset">
        /// The offset of the first frame which is taken for calculations. All other frames will be behind that frame.
        /// </param>
        /// <param name="maxTime">
        /// The maximum for STA iterations. So maxTime defines the number of rows for the resulting receptive field matrix.
        /// </param>
        /// <param name="smoothKernel">
        /// A square matrix representing the smoothing kernel mask used to smooth the result. If this parameter is null,
        /// no smoothing will be done.
        /// </param>
        /// <returns>Returns a 2x2-matrix containing the receptive field. </returns>
        public static double[][] CalculateRF(double[][] stimuli, double[][][] spikes, int offset, int maxTime, double[,] smoothKernel, bool useDynamicDivisorForEdges = false)
        {
            var receptiveField = new double[maxTime][];

            for (var time = 0; time < maxTime; time++)
            {
                var sta = SpikeTriggeredAnalysis.CalculateSTA(stimuli, spikes, offset - time, RoundStrategy.Round);
                receptiveField[time] = sta;
            }

            if (smoothKernel == null)
                return receptiveField;

            var result = Math.Convolution(receptiveField, receptiveField.Length, receptiveField[0].Length, smoothKernel, useDynamicDivisorForEdges);

            return result;
        }

        public static double[][][] CalculateMatches(double[][] stimuli, double[][] receptiveField, MatchOperation matchOperation)
        {
            var dimension = receptiveField.Length;
            var result = new double[stimuli.Length - dimension][][];
            var receptiveFieldMatrix = (new DenseMatrix(1)).OfArray(receptiveField);

            for (var matchIndex = 0; matchIndex < stimuli.Length - dimension; matchIndex++)
            {
                result[matchIndex] = new double[dimension][];

                var stimuliData = new double[dimension][];
                for (var index = 0; index < dimension; index++)
                    stimuliData[index] = stimuli[dimension - index - 1 + matchIndex];

                var stimuliMatrix = (new DenseMatrix(1)).OfArray(stimuliData);

                if (matchOperation == MatchOperation.StaLeftHandWithStimuliRightHand)
                    result[matchIndex] = receptiveFieldMatrix.Multiply(stimuliMatrix).AsArray();
                else if (matchOperation == MatchOperation.StimuliLeftHandWithStaRightHand)
                    result[matchIndex] = stimuliMatrix.Multiply(receptiveFieldMatrix).AsArray();
            }

            return result;
        }

        /// <summary>
        /// This service calculates the spike-triggered covariance for given stimuli data and response spikes data.
        /// </summary>
        /// <param name="stimuli">
        /// The stimuli that triggered spikes as 2-dimensional array with:
        /// [?][ ] - List of stimuli frames.
        /// [ ][?] - Stimulus data for the frame.</param>
        /// <param name="spikes">
        /// The detected spikes for the stimuli as 2-dimensional array with:
        /// [?][ ][ ] - List of cells.
        /// [ ][?][ ] - List of spike data for the cell.
        /// [ ][ ][?] - Time when spike occurred.
        /// </param>
        /// <param name="roundStrategy">
        /// The strategy for rounding the frame numbers.
        /// </param>
        /// <returns>Returns a vector containing the STA. </returns>
        /// <remarks>
        /// Calculation of STC:
        /// 
        ///       N                    T
        /// C =   Σ  [s(tn)-A][s(tn)-A]
        ///      n=1
        ///      -----------------------
        ///                N-1
        /// 
        /// N  = total number of spikes
        /// s  = vector representing the stimuli
        /// tn = time when the nth spike occurred
        /// A  = STA (spike-triggered average)
        /// C  = resulting covariance matrix
        /// </remarks>
        public static double[][] CalculateSTC(double[][] stimuli, double[][][] spikes, RoundStrategy roundStrategy)
        {

            const double frameInterval = 1 / 59.721395; // = 0.016744 ms
            var spikeTriggeredStimulusEnsemble = GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, 0, roundStrategy);
            var sta = CalculateSTA(stimuli, spikes, 0, roundStrategy);
            var n = spikeTriggeredStimulusEnsemble.Length;

            var stc =
                Math.Divide(
                    Math.Sum(
                        Math.Subtract(spikeTriggeredStimulusEnsemble, sta).Select(stimulusReducedBySta => 
                            Math.Tensor(stimulusReducedBySta, stimulusReducedBySta)).ToArray()), 
                    n - 1);

            return stc;
        }

        /// <summary>
        /// This service calculates the Eigen-value decomposition and returns the Eigen-values and -vectors of a given matrix.
        /// </summary>
        /// <param name="matrix">The matrix for which the Eigen-values should be calculated.</param>
        /// <param name="eigenValues">The calculated Eigen-values.</param>
        /// <param name="eigenVectors">The calculated Eigen-vectors.</param>
        public static void CalculateEigenValues(double[][] matrix, out double[] eigenValues, out double[][] eigenVectors)
        {
            var denseMatrix = (new DenseMatrix(1)).OfArray(matrix);
            var evd = denseMatrix.Evd();

            eigenValues = (from value in evd.EigenValues()
                           select value.Real).ToArray<double>();

            eigenVectors = evd.EigenVectors().AsArray();
        }

        /// <summary>
        /// This method creates a list with all stimuli data that elicited spikes.
        /// </summary>
        /// <param name="stimuli">
        /// The stimuli that triggered spikes as 2-dimensional array with:
        /// [?][ ] - List of stimuli frames.
        /// [ ][?] - Stimulus data for the frame.</param>
        /// <param name="spikes">
        /// The detected spikes for the stimuli as 2-dimensional array with:
        /// [?][ ][ ] - List of cells.
        /// [ ][?][ ] - List of spike data for the cell.
        /// [ ][ ][?] - No. of frame which triggered the spike.
        /// </param>
        /// <param name="frameInterval">
        /// The presentation duration of one stimulus frame.
        /// </param>
        /// <param name="frameOffset">
        /// The offset of the first frame which is taken for calculations. All other frames will be behind that frame.
        /// </param>
        /// <param name="roundStrategy">
        /// The strategy for rounding the frame numbers.
        /// </param>
        /// <returns>Returns an array of the stimuli which triggered spikes. </returns>
        public static double[][] GetSpikeTriggeredStimulusEnsemble(double[][] stimuli, double[][][] spikes, double frameInterval, double frameOffset, RoundStrategy roundStrategy)
        {
            var spikeTriggeredStimulusEnsemble = new List<double[]>();
            var spikesAndFrames = new double[0][][];

            if (roundStrategy == RoundStrategy.Ceiling)
                spikesAndFrames = Math.Ceiling(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Floor)
                spikesAndFrames = Math.Floor(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Round)
                spikesAndFrames = Math.Round(Math.Divide(spikes, frameInterval));

            foreach (double[][] cell in spikesAndFrames)
            {
                for (var spike = 0; spike < cell.Length; spike++)
                {
                    var frame = (int)(cell[spike][0] - frameOffset);
                    if (frame > 0 && frame < stimuli.Length)
                        spikeTriggeredStimulusEnsemble.Add(stimuli[frame]);
                }
            }

            return spikeTriggeredStimulusEnsemble.ToArray();
        }

        #endregion
    }
}
