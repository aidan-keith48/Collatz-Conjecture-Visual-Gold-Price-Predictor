using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Collatz_Conjecture_WFA
{
    public class ChartManager
    {
        /// <summary>
        /// Generates a moving average chart.
        /// </summary>
        /// <param name="chart">The chart control to generate the chart on.</param>
        /// <param name="prices">The list of stock prices.</param>
        /// <param name="sma">The list of Simple Moving Average values.</param>
        /// <param name="ema">The list of Exponential Moving Average values.</param>
        /// <param name="period">The period for calculating the moving averages.</param>
        public void GenerateMovingAverageChart(Chart chart, List<double> prices, List<double> sma, List<double> ema, int period)
        {
            chart.Series.Clear();

            // Stock Price Series
            Series priceSeries = new Series("Stock Prices")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };

            // SMA0 Series
            Series smaSeries = new Series("MA0 (SMA from zero)")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Orange
            };

            // EMA0Prime Series
            Series emaSeries = new Series("MA0Prime (EMA from zero)")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Red
            };

            // Add the series to the chart
            chart.Series.Add(priceSeries);
            chart.Series.Add(smaSeries);
            chart.Series.Add(emaSeries);

            // Plot Stock Prices
            for (int i = 0; i < prices.Count; i++)
            {
                priceSeries.Points.AddXY(i, prices[i]);
            }

            // Plot SMA0 (SMA from zero)
            for (int i = 0; i < sma.Count; i++)
            {
                smaSeries.Points.AddXY(i, sma[i]);
            }

            // Plot EMA0Prime (EMA from zero)
            for (int i = 0; i < ema.Count; i++)
            {
                emaSeries.Points.AddXY(i, ema[i]);
            }

            chart.Invalidate();
        }

        public void DrawVerticalLinesAtPeaks(Chart chart, List<int> peaks, List<double> prices)
        {
            foreach (int peakIndex in peaks)
            {
                VerticalLineAnnotation verticalLine = new VerticalLineAnnotation
                {
                    AxisX = chart.ChartAreas[0].AxisX,
                    AxisY = chart.ChartAreas[0].AxisY,
                    X = peakIndex, // Position the line at the peak index
                    LineColor = Color.Yellow, // Set the color for the line
                    LineDashStyle = ChartDashStyle.Dash,
                    LineWidth = 2
                };

                // Add the annotation (line) to the chart
                chart.Annotations.Add(verticalLine);
            }
        }


    }
}
