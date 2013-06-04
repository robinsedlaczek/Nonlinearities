using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;
using AForge.Math;

namespace Nonlinearities.Gui
{
    /// <summary>
    /// This class represents a kernel with some metadata for usage in the gui.
    /// </summary>
    public class KernelGuiElement
    {
        /// <summary>
        /// The name of the kernel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Some description of the kernel and what the kernel is for.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An image representing the kernel (schematically).
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The real mathematically kernel representation.
        /// </summary>
        public double[,] Matrix { get; set; }
    }
}
