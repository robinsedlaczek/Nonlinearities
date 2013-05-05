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
            var reader = new MatFileReader(@"F:\Development\Uni\Nonlinearities\Data\Long Experiment\msq1D.mat");

            var stimuli = ((MLDouble)reader.GetMLArray("msq1D")).GetArray();

            reader = new MatFileReader(@"F:\Development\Uni\Nonlinearities\Data\Long Experiment\spksV6.mat");

            var spikes1 = ((MLDouble)reader.Data[0]).GetArray();
            var spikes2 = ((MLDouble)reader.Data[1]).GetArray();
            var spikes3 = ((MLDouble)reader.Data[2]).GetArray();
            var spikes4 = ((MLDouble)reader.Data[3]).GetArray();
            
        }

    }
}
