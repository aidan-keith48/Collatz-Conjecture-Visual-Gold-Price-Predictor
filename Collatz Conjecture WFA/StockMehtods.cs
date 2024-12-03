using System;
using System.Collections.Generic;
using System.Linq;

namespace Collatz_Conjecture_WFA
{
    public class StockMethods
    {
        /// <summary>
        /// Calculates the Simple Moving Average (SMA) for a given list of prices and a period.
        /// </summary>
        /// <param name="prices">The list of prices.</param>
        /// <param name="period">The period for calculating the SMA.</param>
        /// <returns>The list of SMA values.</returns>
        public List<double> CalculateSMAFromZero(List<double> prices, int period)
        {
            List<double> sma = new List<double>();

            // Progressive SMA that starts at the first point
            for (int i = 0; i < prices.Count; i++)
            {
                int start = Math.Max(0, i - period + 1); // Ensure we don't exceed array bounds
                double sum = 0;
                for (int j = start; j <= i; j++)
                {
                    sum += prices[j];
                }
                sma.Add(sum / (i - start + 1)); // Average for the available points
            }

            return sma;
        }


        /// <summary>
        /// Calculates the Exponential Moving Average (EMA) for a given list of prices and a period.
        /// </summary>
        /// <param name="prices">The list of prices.</param>
        /// <param name="period">The period for calculating the EMA.</param>
        /// <returns>The list of EMA values.</returns>
        public List<double> CalculateEMAFromZero(List<double> prices, int period)
        {
            List<double> ema = new List<double>();
            double multiplier = 2.0 / (period + 1);
            double previousEma = prices[0]; // Start the EMA from the first point

            ema.Add(previousEma); // EMA starts at the first point (price[0])

            for (int i = 1; i < prices.Count; i++)
            {
                double currentEma = ((prices[i] - previousEma) * multiplier) + previousEma;
                ema.Add(currentEma);
                previousEma = currentEma;
            }

            return ema;
        }

        public List<int> FindPeaks(List<double> prices)
        {
            List<int> peaks = new List<int>();

            for (int i = 1; i < prices.Count - 1; i++)
            {
                if (prices[i] > prices[i - 1] && prices[i] > prices[i + 1])
                {
                    peaks.Add(i); // Store the index of the peak
                }
            }

            return peaks;
        }

    }
}
