using csmatio.io;
using csmatio.types;
using System.Collections.Generic;

namespace Nonlinearities.Analysis
{
    /// <summary>
    /// This class loads some data and provides this data in multidimensional arrays to consumers.
    /// </summary>
    public static class DataLoader
    {
        /// <summary>
        /// This method provides spikes data.
        /// </summary>
        /// <returns>
        /// Returns the spikes caused by stimuli delivered by the GetStimuli-method. 
        /// 
        /// Dimensions of the returned array:
        /// 1 - Observed cells.
        /// 2 - Detected spikes for each cell.
        /// 3 - Time when spike occurred.
        /// </returns>
        public static double[][][] GetSpikes()
        {
            var reader = new MatFileReader(@"F:\Development\Uni\Nonlinearities\Data\Long Experiment\spksV6.mat");
            // var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\spksV6.mat");
            var spikes = new List<double[][]>();

            reader.Data.ForEach(data => spikes.Add(((MLDouble)data).GetArray()));

            return spikes.ToArray();
        }

        /// <summary>
        /// This method provides stimuli data. 
        /// </summary>
        /// <returns>
        /// Returns stimuli which triggered spikes delivered by the GetSpikes-method.
        /// 
        /// Dimension of the returned array:
        /// 1 - Presented frames.
        /// 2 - Presented bars per frame (1 represents white bar, -1 represents black bar).
        /// </returns>
        public static double[][] GetStimuli()
        {
            var reader = new MatFileReader(@"F:\Development\Uni\Nonlinearities\Data\Long Experiment\msq1D.mat");
            //var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\msq1D.mat");
            return ((MLDouble)reader.GetMLArray("msq1D")).GetArray();
        }

    }
}
