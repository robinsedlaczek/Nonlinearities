using System;
using System.Collections.Generic;
using System.Linq;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class provides services for Spike-triggered Analysis.
    /// </summary>
    public static class SpikeTriggeredAnalysis
    {
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
        private static double[][] GetSpikeTriggeredStimulusEnsemble(double[][] stimuli, double[][][] spikes, double frameInterval, int frameOffset, RoundStrategy roundStrategy)
        {
            var spikeTriggeredStimulusEnsemble = new List<double[]>();
            var spikesAndFrames = new double[0][][];

            if (roundStrategy == RoundStrategy.Ceiling)
                spikesAndFrames = Math.Ceiling(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Floor)
                spikesAndFrames = Math.Floor(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Round)
                spikesAndFrames = Math.Round(Math.Divide(spikes, frameInterval));

            // [RS] Take same stimulus as often as a spike was detected for this stimulus?
            //      From all cells?
            foreach (double[][] cell in spikesAndFrames)
                spikeTriggeredStimulusEnsemble.AddRange(cell.Select(spike => (int)spike[0]).Select(frame => stimuli[frame + frameOffset]));

            return spikeTriggeredStimulusEnsemble.ToArray();
        }
    }
}
