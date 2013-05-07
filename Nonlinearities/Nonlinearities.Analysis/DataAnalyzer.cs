using csmatio.io;
using csmatio.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonlinearities.Analysis
{
    public class DataAnalyzer
    {
        public DataAnalyzer()
        {

        }

        public static List<double[][]> GetSpikes()
        {
            var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\spksV6.mat");

            return new List<double[][]>()
            {
                ((MLDouble)reader.Data[0]).GetArray(),
                ((MLDouble)reader.Data[1]).GetArray(),
                ((MLDouble)reader.Data[2]).GetArray(),
                ((MLDouble)reader.Data[3]).GetArray()
            };
        }

        public static double[][] GetStimuli()
        {
            var reader = new MatFileReader(@"..\..\..\Data\Long Experiment\msq1D.mat");
            return ((MLDouble)reader.GetMLArray("msq1D")).GetArray();
        }

    }
}
