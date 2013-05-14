using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="roundStrategy">
        /// The strategy for rounding the frame numbers.
        /// </param>
        /// <returns>Returns a vector containing the STA. </returns>
        /// <remarks>
        /// Calculation of STA:
        /// 1. frameInterval = 1/59.721395 = 0.016744 ms
        /// 2. spikeTriggeredStimuliEnsemble = round(spikesOfAllCells / frameInterval);
        /// 3. STA = mean(stimuli(framesOfInterest, :));
        /// </remarks>
        public static double[] CalculateSTA(double[][] stimuli, double[][][] spikes, RoundStrategy roundStrategy)
        {
            // 1. frameInterval = 1/59.721395 = 0.016744 ms
            var frameInterval = 1 / 59.721395; // = 0.016744 ms

            // 2. spikeTriggeredStimuliEnsemble = round(spikesOfAllCells / frameInterval);
            // 3. STA = mean(stimuli(spikeTriggeredStimuliEnsemble, :));

            if (roundStrategy == RoundStrategy.Ceiling)
                return Math.Mean(GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, roundStrategy));
            else if (roundStrategy == RoundStrategy.Floor)
                return Math.Mean(GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, roundStrategy));
            else if (roundStrategy == RoundStrategy.Round)
                return Math.Mean(GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, roundStrategy));

            return null;
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
        /// <param name="roundStrategy">
        /// The strategy for rounding the frame numbers.
        /// </param>
        /// <returns>Returns an array of the stimuli which triggered spikes. </returns>
        private static double[][] GetSpikeTriggeredStimulusEnsemble(double[][] stimuli, double[][][] spikes, double frameInterval, RoundStrategy roundStrategy)
        {
            var spikeTriggeredStimulusEnsemble = new List<double[]>();
            double[][][] spikesAndFrames = null;

            if (roundStrategy == RoundStrategy.Ceiling)
                spikesAndFrames = Math.Ceiling(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Floor)
                spikesAndFrames = Math.Floor(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Round)
                spikesAndFrames = Math.Round(Math.Divide(spikes, frameInterval));

            // [RS] Take same stimulus as often as a spike was detected for this stimulus?
            //      From all cells?
            for (var indexA = 0; indexA < spikesAndFrames.Length; indexA++)
            {
                for (var indexB = 0; indexB < spikesAndFrames[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < spikesAndFrames[indexA][indexB].Length; indexC++)
                    {
                        var frame = (int)spikesAndFrames[indexA][indexB][indexC];
                        var stimulus = stimuli[frame];
                        spikeTriggeredStimulusEnsemble.Add(stimulus);
                    }
                }
            }

            return spikeTriggeredStimulusEnsemble.ToArray();
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
        /// Calculation of STA:
        /// 
        /// 
        /// </remarks>
        public static double[][] CalculateSTC(double[][] stimuli, double[][][] spikes, RoundStrategy roundStrategy)
        {
            var frameInterval = 1 / 59.721395; // = 0.016744 ms
            var spikeTriggeredStimulusEnsemble = GetSpikeTriggeredStimulusEnsemble(stimuli, spikes, frameInterval, roundStrategy);
            var sta = Math.Mean(spikeTriggeredStimulusEnsemble);
            



            return null;
        }
    }
}
