using System.Windows.Threading;
using Nonlinearities.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private double[][][] _spikes;
        private Timer _animationTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoadStimuliDataButtonClick(object sender, RoutedEventArgs e)
        {
            _stimuli = DataLoader.GetStimuli();
            _spikes = DataLoader.GetSpikes();

            InitializeStimuliView();

            const int offset = 15;
            var imageData = new double[offset][];

            for (var time = 0; time < 15; time++)
            {
                var sta = SpikeTriggeredAnalysis.CalculateSTA(_stimuli, new double[][][] { _spikes[0] }, offset - time, RoundStrategy.Round);
                imageData[time] = sta;
            }

            var rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.Black,
                Width = 150,
                Height = 50
            };

            Canvas.SetLeft(rect, 50);
            Canvas.SetTop(rect, 50);

            ReceptiveFieldPlotCanvas.Children.Add(rect);
        }

        private void InitializeStimuliView()
        {
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

        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            if (_animationTimer == null)
                _animationTimer = new Timer(OnAnimationTimerTick, null, 17, 17);
            else
                _animationTimer.Change(17, 17);
        }

        private void OnAnimationTimerTick(object state)
        {
            StimuliSlider.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    if (StimuliSlider.Value <= StimuliSlider.Maximum)
                        StimuliSlider.Value++;
                    else
                        _animationTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }));
        }

        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            if (_animationTimer != null)
                _animationTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    } 
}
