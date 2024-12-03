using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Collatz_Conjecture_WFA
{
    public partial class Temp2 : Form
    {
        private StockMethods stockMethods;
        private ChartManager chartManager;

        ConjectureLogic conjectureLogic = new ConjectureLogic();
        public Temp2()
        {
            InitializeComponent();
            stockMethods = new StockMethods();
            chartManager = new ChartManager();
        }

        /// <summary>
        /// Generates and plots the data.
        /// </summary>
        private void GenerateAndPlotData()
        {
            int period = 0; // The period for the moving averages
            int dataCount = 200; // Number of data points

            // Generate random stock prices
            List<double> prices = GenerateRandomStockPrices(dataCount);

            // Generate and plot the chart
            GenerateMovingAverageChart(prices, period);
        }
        //---------------------------------------------------------------------------

        /// <summary>
        /// Generates random stock prices.
        /// </summary>
        /// <param name="count">The number of stock prices to generate.</param>
        /// <returns>A list of randomly generated stock prices.</returns>
        private List<double> GenerateRandomStockPrices(int count)
        {
            List<double> prices = new List<double>();
            Random random = new Random();
            double price = 0; // Starting price

            for (int i = 0; i < count; i++)
            {
                // Simulate stock price changes
                double change = random.NextDouble() * 4 - 2; // Random change between -2 and 2
                price += change;

                // Ensure the price does not dip below 0
                if (price < 0)
                {
                    price = 0;
                }

                prices.Add(price);
            }

            return prices;
        }
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Generates the moving average chart.
        /// </summary>
        /// <param name="prices">The list of stock prices.</param>
        /// <param name="period">The period for calculating the moving average.</param>
        private void GenerateMovingAverageChart(List<double> prices, int period)
        {
            // Calculate SMA and EMA starting from the first point (MA0 and MA0Prime)
            List<double> smaFromZero = stockMethods.CalculateSMAFromZero(prices, period);
            List<double> emaFromZero = stockMethods.CalculateEMAFromZero(prices, period);

            // Generate and plot the chart with both SMA0 and EMA0Prime moving averages
            chartManager.GenerateMovingAverageChart(AverageLineChart, prices, smaFromZero, emaFromZero, period);

            // Find peaks in the stock prices
            List<int> peaks = stockMethods.FindPeaks(prices);

            // Draw vertical lines at the peaks
            chartManager.DrawVerticalLinesAtPeaks(AverageLineChart, peaks, prices);
        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Clears all series from the chart.
        /// </summary>
        private void ClearAllBtn_Click(object sender, EventArgs e)
        {
            ConjectureChart.Series.Clear();
        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Runs the Collatz solution and updates the UI.
        /// </summary>
        private void RunSolutionBtn_Click(object sender, EventArgs e)
        {
            GenerateCollatzChart();
            ListSize.Text = "List Size: " + conjectureLogic.dataListPublic.Count;
            ListTxt.Text = "List: " + conjectureLogic.DisplayList();
        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Generates the Collatz chart.
        /// </summary>
        private void GenerateCollatzChart()
        {
            int startNumber = (int)numberPicker.Value;

            if (startNumber == 0)
            {
                MessageBox.Show("Invalid input. Please enter a non-zero number.");
                return;
            }

            List<long> sequence = conjectureLogic.CollatzSequence(startNumber);

            ConjectureChart.Series.Clear();
            Series series = new Series
            {
                Name = "Collatz Sequence",
                ChartType = SeriesChartType.Line
            };

            ConjectureChart.Series.Add(series);

            for (int i = 0; i < sequence.Count; i++)
            {
                series.Points.AddXY(i, sequence[i]);
            }

            ConjectureChart.Invalidate();
        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the line chart checkbox change event.
        /// </summary>
        private void lineChart_CheckedChanged(object sender, EventArgs e)
        {
            if (lineChart.Checked)
            {
                foreach (var series in ConjectureChart.Series)
                {
                    series.ChartType = SeriesChartType.Line;
                }
            }

            if (BarHraphCart.Checked)
            {
                foreach (var series in ConjectureChart.Series)
                {
                    series.ChartType = SeriesChartType.Bar;
                }
            }

            if (ColumnChart.Checked)
            {
                foreach (var series in ConjectureChart.Series)
                {
                    series.ChartType = SeriesChartType.Column;
                }
            }

            ConjectureChart.Refresh();
        }
        //----------------------------------------------------------------------------------------

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //----------------------------------------------------------------------------------------

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the run button click event.
        /// </summary>
        private void RunBtn_Click(object sender, EventArgs e)
        {
            // Generate and plot the data when the form loads
            GenerateAndPlotData();
        }
        //----------------------------------------------------------------------------------------
    }
}
