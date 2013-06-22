using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdMath = System.Math;

namespace Nonlinearities.Analysis
{
    public class NormalDistribution
    {
        public NormalDistribution(double mean, double variance)
        {
            Mean = mean;
            Variance = variance;
        }

        public double Mean 
        { 
            get; 
            set; 
        }

        public double Variance 
        { 
            get; 
            set; 
        }

        public double[,] CalculateDensityCurve(int points, double lowerBound, double upperBound)
        {
            var result = new double[points, 2];
            var stepWidth = (upperBound - lowerBound) / points;

            for (var index = 0; index < points; index++)
            {
                var x = lowerBound + index * stepWidth;
                var px = (1d / StdMath.Sqrt(2d * StdMath.PI * Variance)) *
                         StdMath.Exp((-StdMath.Pow(x - Mean, 2)) / (2 * Variance));

                result[index, 0] = x;
                result[index, 1] = px;
            }

            return result;
        }
        
    }
}
