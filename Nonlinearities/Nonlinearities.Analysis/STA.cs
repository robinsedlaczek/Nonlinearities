using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class calculates the STA for given stimuli and response spikes.
    /// </summary>
    public static class STA
    {
        /// <summary>
        /// This service calculates the STA for given stimuli data and response spikes data.
        /// </summary>
        /// <param name="stimuli">
        /// The stimuli that triggered spikes as 2-dimensional array with:
        /// [?][ ] - List of stimuli frames.
        /// [ ][?] - Stimulus data for the frame.</param>
        /// <param name="spikes">
        /// The detected spikes for the stimuli as 2-dimensional array with:
        /// [?][ ][ ] - List of cells.
        /// [ ][?][ ] - List of spike data for the cell.
        /// [ ][ ][?] - Data item of spike data.
        /// </param>
        /// <returns>Returns a vector containing the STA. </returns>
        /// <remarks>
        /// Calculation of STA:
        /// 1. frameInterval = 1/59.721395 = 0.016744 ms
        /// 2. spikeTriggeredStimuliEnsemble = round(spikesOfAllCells / frameInterval);
        /// 3. STA = mean(stimuli(framesOfInterest, :));
        /// </remarks>
        public static double[] Calculate(double[][] stimuli, double[][][] spikes, RoundStrategy roundStrategy)
        {
            // 1. frameInterval = 1/59.721395 = 0.016744 ms
            var frameInterval = 1 / 59.721395; // = 0.016744 ms

            // 2. spikeTriggeredStimuliEnsemble = round(spikesOfAllCells / frameInterval);
            double[][][] spikeTriggeredStimulusEnsemble = null;

            if (roundStrategy == RoundStrategy.Ceiling)
                spikeTriggeredStimulusEnsemble = Math.Ceiling(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Floor)
                spikeTriggeredStimulusEnsemble = Math.Floor(Math.Divide(spikes, frameInterval));
            else if (roundStrategy == RoundStrategy.Round)
                spikeTriggeredStimulusEnsemble = Math.Round(Math.Divide(spikes, frameInterval));

            // 3. STA = mean(stimuli(spikeTriggeredStimuliEnsemble, :));
            var stimuliOfInterest = new List<double[]>();

            // [RS] Take same stimulus as often as a spike was detected for this stimulus?
            //      From all cells?
            for (var indexA = 0; indexA < spikes.Length; indexA++)
            {
                for (var indexB = 0; indexB < spikes[indexA].Length; indexB++)
                {
                    for (var indexC = 0; indexC < spikes[indexA][indexB].Length; indexC++)
                    {
                        var stimulus = stimuli[(int)spikeTriggeredStimulusEnsemble[indexA][indexB][indexC]];
                        stimuliOfInterest.Add(stimulus);
                    }
                }
            }

            return Math.Mean(stimuliOfInterest.ToArray<double[]>());
        }
    }
}
