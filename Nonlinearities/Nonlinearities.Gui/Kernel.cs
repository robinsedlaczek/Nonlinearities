using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;
using AForge.Math;

namespace Nonlinearities.Gui
{
    public class Kernel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double[,] Matrix { get; set; }
    }
}
