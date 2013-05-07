using Nonlinearities.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nonlinearities.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double[][] _stimuli;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoadStimuliDataButtonClick(object sender, RoutedEventArgs e)
        {
            _stimuli = DataAnalyzer.GetStimuli();
            var columns = _stimuli[0].Length;

            for (int index = 0; index < columns; index++)
            {
                StimuliDisplayGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                var color = _stimuli[0][index] == -1 ? Colors.Black : Colors.White;
                var border = new Border() { Background = new SolidColorBrush(color) };
                
                StimuliDisplayGrid.Children.Add(border);
                border.SetValue(Grid.ColumnProperty, index);
            }

            StimuliSlider.Minimum = 0;
            StimuliSlider.Maximum = _stimuli.Length - 1;
        }

        private void OnStimuliSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var stimulus = _stimuli[(int)StimuliSlider.Value];

            for (int index = 0; index < stimulus.Length; index++ )
            {
                var color = stimulus[index] == -1 ? Colors.Black : Colors.White;
                (StimuliDisplayGrid.Children[index] as Border).Background = new SolidColorBrush(color);
            }
        }

    } 
}
