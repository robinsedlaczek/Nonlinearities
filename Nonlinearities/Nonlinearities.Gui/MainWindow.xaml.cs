using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AForge.Math;
using Nonlinearities.Analysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.ViewportRestrictions;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.ComponentModel;
using System.Data;
using Nonlinearities.Gui.Properties;

namespace Nonlinearities.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Private Fields

        private double[][] _stimuli;
        private double[][][] _spikes;
        private double[][] _receptiveField;
        private DataTable _numericData;
        private Timer _animationTimer;
        private List<LineAndMarker<ElementMarkerPointsGraph>> _eigenvaluesGraphs;
        private List<Kernel> _kernels;

        #endregion

        #region Interface IPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Construction

        public MainWindow()
        {
            LoadKernels();

            DataContext = this;

            InitializeComponent();
        }

        #endregion

        #region Public Members

        public DataTable NumericData
        {
            get
            {
                return _numericData;
            }

            private set
            {
                if (_numericData != value)
                    _numericData = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumericData"));
            }
        }

        public List<Kernel> Kernels
        {
            get
            {
                return _kernels;
            }
        }

        #endregion

        #region Event Handler

        private void OnStimuliSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var stimulus = _stimuli[(int)StimuliSlider.Value];

            for (int index = 0; index < stimulus.Length; index++)
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

            var imageData = GetReceptiveFieldPlotData(false);
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell1CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell2CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell3CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnCell4CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnOffsetTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null)
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnTimeTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null)
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnPCACellCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            UpdateEigenvalueGraphVisibility();
        }

        private void OnStimuliTabItemGotFocus(object sender, RoutedEventArgs e)
        {
            if (_stimuli == null)
                return;

            var dataTable = new DataTable("Stimuli");
            dataTable.Columns.AddRange((from stimulus in _stimuli[0] select new DataColumn(string.Empty, typeof(double))).ToArray());

            foreach (var stimulus in _stimuli)
            {
                var objectArray = new object[stimulus.Length];
                stimulus.CopyTo(objectArray, 0);

                dataTable.Rows.Add(objectArray);
            }

            NumericData = dataTable;
        }

        private void OnReceptiveFieldTabItemGotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void OnPrincipalComponentsAnalysisTabItemGotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _stimuli = DataLoader.GetStimuli();
            _spikes = DataLoader.GetSpikes();

            InitializeStimuliView();
            InitializeReceptiveFieldPlotView();
            InitializeEigenvaluesPlot();
        }

        private void OnLocationChanged(object sender, System.EventArgs e)
        {

        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void OnSmoothCheckBoxClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null)
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnDynamicDevisorCheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null)
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private void OnSmoothKernelComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReceptiveFieldPlotCanvas != null)
                ReceptiveFieldPlotCanvas.Children.Clear();

            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        #endregion

        #region Private Members

        private void LoadKernels()
        {
            _kernels = new List<Kernel>()
                {
                    new Kernel()
                        {
                            Name = "All Neighbors",
                            Description = "A normal blur filter is a type of image-blurring that uses all neighbors for calculating the transformation to apply to each pixel in the image.",
                            Image = "Resources/AllNeighbors.png",
                            Matrix = new double[3,3]
                                {
                                    { 1, 1, 1 },
                                    { 1, 1, 1 },
                                    { 1, 1, 1 }
                                }
                        },

                    new Kernel()
                        {
                            Name = "Gaussian",
                            Description = "The Gaussian blur is a type of image-blurring filter that uses a Gaussian function for calculating the transformation to apply to each pixel in the image.",
                            Image = "Resources/Gauss 02.png",
                            Matrix = (new Gaussian()).Kernel2D(3)
                        }
                };
        }

        private void InitializeEigenvaluesPlot()
        {
            if (_eigenvaluesGraphs == null)
                _eigenvaluesGraphs = new List<LineAndMarker<ElementMarkerPointsGraph>>();

            var graph = PlotEigenvalues(1, Brushes.Blue);
            _eigenvaluesGraphs.Add(graph);

            graph = PlotEigenvalues(2, Brushes.Red);
            _eigenvaluesGraphs.Add(graph);

            graph = PlotEigenvalues(3, Brushes.Green);
            _eigenvaluesGraphs.Add(graph);

            graph = PlotEigenvalues(4, Brushes.HotPink);
            _eigenvaluesGraphs.Add(graph);

            UpdateEigenvalueGraphVisibility();
        }

        private void UpdateEigenvalueGraphVisibility()
        {
            _eigenvaluesGraphs[0].LineGraph.Visibility = BoolToVisibility(PCACell1CheckBox.IsChecked);
            _eigenvaluesGraphs[0].MarkerGraph.Visibility = BoolToVisibility(PCACell1CheckBox.IsChecked);
            _eigenvaluesGraphs[1].LineGraph.Visibility = BoolToVisibility(PCACell2CheckBox.IsChecked);
            _eigenvaluesGraphs[1].MarkerGraph.Visibility = BoolToVisibility(PCACell2CheckBox.IsChecked);
            _eigenvaluesGraphs[2].LineGraph.Visibility = BoolToVisibility(PCACell3CheckBox.IsChecked);
            _eigenvaluesGraphs[2].MarkerGraph.Visibility = BoolToVisibility(PCACell3CheckBox.IsChecked);
            _eigenvaluesGraphs[3].LineGraph.Visibility = BoolToVisibility(PCACell4CheckBox.IsChecked);
            _eigenvaluesGraphs[3].MarkerGraph.Visibility = BoolToVisibility(PCACell4CheckBox.IsChecked);
        }

        private Visibility BoolToVisibility(bool? visible)
        {
            if (visible != null && visible.Value)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Hidden;
        }

        private LineAndMarker<ElementMarkerPointsGraph> PlotEigenvalues(int cell, Brush brush)
        {
            double[] eigenValues;
            double[][] eigenVectors;

            var stc = SpikeTriggeredAnalysis.CalculateSTC(_stimuli, new double[][][] { _spikes[cell - 1] }, RoundStrategy.Round);
            SpikeTriggeredAnalysis.CalculateEigenValues(stc, out eigenValues, out eigenVectors);
            
            // Prepare data in arrays
            var N = eigenValues.Length;
            var x = new double[N];
            var y = new double[N];

            for (var index = 0; index < N; index++)
            {
                x[index] = index;
                y[index] = eigenValues[index];
            }

            // Add data sources:
            var yDataSource = new EnumerableDataSource<double>(y);
            yDataSource.SetYMapping(Y => Y);
            yDataSource.AddMapping(ShapeElementPointMarker.ToolTipTextProperty, Y => string.Format("Cell {0} - Eigenvalue \n\n{1}", cell, Y));

            var xDataSource = new EnumerableDataSource<double>(x);
            xDataSource.SetXMapping(X => X);

            var compositeDataSource = new CompositeDataSource(xDataSource, yDataSource);

            EigenvaluePlotter.Viewport.Restrictions.Add(new PhysicalProportionsRestriction { ProportionRatio = 30 });
            var graph = EigenvaluePlotter.AddLineGraph(compositeDataSource, new Pen(brush, 1), new SampleMarker() { Brush = brush }, new PenDescription(string.Format("Eigenvalues of Cell {0}", cell)));

            return graph;
        }

        private void InitializeReceptiveFieldPlotView()
        {
            var imageData = GetReceptiveFieldPlotData();
            PlotReceptiveField(imageData, ReceptiveFieldPlotCanvas);
        }

        private double[][] GetReceptiveFieldPlotData(bool recalcData = true)
        {
            if (_spikes == null)
                return null;

            if (!recalcData)
                return _receptiveField;

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

            double[,] smoothKernel = null;
            bool useDynamicDivisorForEdges = false;

            if (SmoothCheckBox.IsChecked.HasValue && SmoothCheckBox.IsChecked.Value)
            {
                useDynamicDivisorForEdges = DynamicDevisorCheckBox.IsChecked != null && DynamicDevisorCheckBox.IsChecked.Value;

                if (SmoothKernelComboBox.SelectedIndex == 0)
                {
                    smoothKernel = new double[3, 3]
                        {
                            { 1, 1, 1 },
                            { 1, 1, 1 },
                            { 1, 1, 1 }
                        };
                }
                else if (SmoothKernelComboBox.SelectedIndex==1)
                {
                    smoothKernel = (new Gaussian()).Kernel2D(3);
                }
            }

            _receptiveField = SpikeTriggeredAnalysis.CalculateRF(_stimuli, spikes.ToArray(), offset, maxTime, smoothKernel, useDynamicDivisorForEdges);

            return _receptiveField;
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

        #endregion
    } 
}
