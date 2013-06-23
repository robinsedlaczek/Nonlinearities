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
        #region Private Fields

        private int _points;
        private double _mean;
        private double _variance;
        private double _lowerBound;
        private double _upperBound;
        private bool _parameterChanged;
        private double[,] _densityCurve;

        #endregion

        #region Construction

        public NormalDistribution(double mean, double variance, int points, double lowerBound, double upperBound)
        {
            _mean = mean;
            _variance = variance;
            _points = points;
            _lowerBound = lowerBound;
            _upperBound = upperBound;

            _parameterChanged = true;
        }

        #endregion

        #region Public Members

        public double Mean 
        { 
            get
            {
                return _mean;
            }

            set
            {
                if (_mean != value)
                {
                    _mean = value;
                    _parameterChanged = true;
                }
            }
        }

        public double Variance 
        {
            get
            {
                return _variance;
            }

            set
            {
                if (_variance != value)
                {
                    _variance = value;
                    _parameterChanged = true;
                }
            }
        }

        public int Points 
        {
            get
            {
                return _points;
            }

            set
            {
                if (_points != value)
                {
                    _points = value;
                    _parameterChanged = true;
                }
            }
        }

        public double LowerBound 
        {
            get
            {
                return _lowerBound;
            }

            set
            {
                if (_lowerBound != value)
                {
                    _lowerBound = value;
                    _parameterChanged = true;
                }
            }
        }

        public double UpperBound 
        {
            get
            {
                return _upperBound;
            }

            set
            {
                if (_upperBound != value)
                {
                    _upperBound = value;
                    _parameterChanged = true;
                }
            }
        }

        public double[,] DensityCurve
        {
            get
            {
                if (_parameterChanged)
                {
                    _densityCurve = CalculateDensityCurve();
                    _parameterChanged = false;
                }

                return _densityCurve;
            }
        }

        #endregion

        #region Operators

        public static Nonlinearity operator / (NormalDistribution numerator, NormalDistribution denominator)
        {
            return null;
        }

        #endregion

        #region Private Members

        private double[,] CalculateDensityCurve()
        {
            var result = new double[Points, 2];
            var stepWidth = (UpperBound - LowerBound) / Points;

            for (var index = 0; index < Points; index++)
            {
                var x = LowerBound + index * stepWidth;
                var px = (1d / StdMath.Sqrt(2d * StdMath.PI * Variance)) *
                         StdMath.Exp((-StdMath.Pow(x - Mean, 2)) / (2 * Variance));

                result[index, 0] = x;
                result[index, 1] = px;
            }

            return result;
        }

        #endregion
    }
}
