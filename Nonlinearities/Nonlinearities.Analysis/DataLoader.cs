using csmatio.io;
using csmatio.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonlinearities.Analysis
{
    public class DataLoader
    {
        public static double[][][] GetSpikes()
        {
            var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\spksV6.mat");
            var spikes = new List<double[][]>();

            reader.Data.ForEach(data => spikes.Add((data as MLDouble).GetArray()));

            return spikes.ToArray();
        }

        public static double[][] GetStimuli()
        {
            var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\msq1D.mat");
            return ((MLDouble)reader.GetMLArray("msq1D")).GetArray();
        }

    }
}
