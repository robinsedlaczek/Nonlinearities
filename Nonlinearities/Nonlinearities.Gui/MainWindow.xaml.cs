using System.Windows.Threading;
using Nonlinearities.Analysis;
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
        private double[][] _imageData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoadStimuliDataButtonClick(object sender, RoutedEventArgs e)
        {
            _stimuli = DataLoader.GetStimuli();
            _spikes = DataLoader.GetSpikes();

            InitializeStimuliView();
            InitializeReceptiveFieldPlotView();
        }

        private void InitializeReceptiveFieldPlotView()
        {
            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private double[][] GetPlotData(bool recalcData = true)
        {
            if (_spikes == null)
                return null;

            if (!recalcData)
                return _imageData;

            var spikes = new List<double[][]>();

            if (Cell1CheckBox.IsChecked != null && Cell1CheckBox.IsChecked.Value)
                spikes.Add(_spikes[0]);

            if (Cell2CheckBox.IsChecked != null && Cell2CheckBox.IsChecked.Value)
                spikes.Add(_spikes[1]);

            if (Cell3CheckBox.IsChecked != null && Cell3CheckBox.IsChecked.Value)
                spikes.Add(_spikes[2]);

            if (Cell4CheckBox.IsChecked != null && Cell4CheckBox.IsChecked.Value)
                spikes.Add(_spikes[3]);

            if (spikes.Count == 0)
                return null;

            int offset;
            if (!int.TryParse(OffsetTextbox.Text, out offset))
                offset = 16;

            int maxTime;
            if (!int.TryParse(TimeTextbox.Text, out maxTime))
                maxTime = 16;

            _imageData = new double[maxTime][];

            for (var time = 0; time < maxTime; time++)
            {
                var sta = SpikeTriggeredAnalysis.CalculateSTA(_stimuli, spikes.ToArray(), offset - time, RoundStrategy.Round);
                _imageData[time] = sta;
            }

            return _imageData;
        }

        private void PlotReceptiveField(double[][] imageData, Canvas plotCanvas)
        {
            if (imageData == null)
                return;

            double minimum;
            double maximum;
            Math.MinMax(imageData, out minimum, out maximum);

            var dataHeight = imageData.Length;
            var dataWidth = imageData[0].Length;

            var rasterHeight = plotCanvas.ActualHeight / dataHeight;
            var rasterWidth = plotCanvas.ActualWidth / dataWidth;

            for (var y = 0; y < dataHeight; y++)
            {
                for (var x = 0; x < dataWidth; x++)
                {
                    var alpha = (byte)(((imageData[y][x] - minimum) * 255) / (maximum - minimum));

                    var rect = new Rectangle
                    {
                        Fill = new SolidColorBrush(Color.FromArgb(255, alpha, alpha, alpha)),
                        Width = rasterWidth,
                        Height = rasterHeight
                    };

                    Canvas.SetTop(rect, y * rasterHeight);
                    Canvas.SetLeft(rect, x * rasterWidth);

                    plotCanvas.Children.Add(rect);
                }
            }
        }

        private void InitializeStimuliView()
        {
            var columns = _stimuli[0].Length;

            for (int index = 0; index < columns; index++)
            {
                StimuliDisplayGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                const double epsilon = 0.00001;
                var color = System.Math.Abs(_stimuli[0][index] - -1) < epsilon ? Colors.Black : Colors.White;
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
                const double epsilon = 0.00001;
                var color = System.Math.Abs(stimulus[index] - -1) < epsilon ? Colors.Black : Colors.White;
                var border = StimuliDisplayGrid.Children[index] as Border;

                if (border != null) 
                    border.Background = new SolidColorBrush(color);
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
            StimuliSlider.Dispatcher.Invoke(DispatcherPriority.Normal, new System.Action(() =>
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

        private void OnReceptiveFieldPlotCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData(false);
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell1CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell2CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell3CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell4CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnOffsetTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null) 
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnTimeTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null) 
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }
    } 
}
