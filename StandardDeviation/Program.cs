using System;

namespace StandardDeviation {
    class Program
    {

        private static double[] data;
        private static double[] data2;

        static void Main(string[] args)
        {

            data = new double[]
            {
                6, 12, 7, 16, 4
            };

            data2 = new double[]
            {
                4, 1, 3, 2, 9
            };

            double[] dataUnsorted = new double[data.Length];
            data.CopyTo(dataUnsorted, 0);
            double[] data2Unsorted = new double[data2.Length];
            data2.CopyTo(data2Unsorted, 0);

            Array.Sort(data);
            Array.Sort(data2);

            double range = data[data.Length - 1] - data[0];
            double mid = GetMedian(data);
            double mean = GetMean(data);
            double sd = CalculateStandardDeviationP(data, mean);

            Console.WriteLine("[1] Median: " + mid);
            Console.WriteLine("[1] Range: " + range);
            Console.WriteLine("[1] Mean: " + mean);
            Console.WriteLine("[1] Standard Deviation: " + sd);
            Console.WriteLine("[1] Z-Score: " + GetZScore(data, mean, sd));
            Console.WriteLine();


            double range2 = data2[data2.Length - 1] - data2[0];
            double mid2 = GetMedian(data2);
            double mean2 = GetMean(data2);
            double sd2 = CalculateStandardDeviationP(data2, mean2);

            Console.WriteLine("[2] Median: " + mid2);
            Console.WriteLine("[2] Range: " + range2);
            Console.WriteLine("[2] Mean: " + mean2);
            Console.WriteLine("[2] Standard Deviation: " + sd2);
            Console.WriteLine("[2] Z-Score: " + GetZScore(data2, mean2, sd2));
            Console.WriteLine();

            double pearsons = PearsonCorrelation(dataUnsorted, data2Unsorted, mean, mean2, sd, sd2);
            string valuation = "";

            if (Math.Abs(pearsons) >= 0.7) valuation += "STRONG";
            else if (Math.Abs(pearsons) >= 0.3)
                valuation += "WEAK";
            else
                valuation += "LITTLE OR NO";


            if (pearsons < 0) valuation += " NEGATIVE CORRELATION";
            else valuation += " POSITIVE CORRELATION";

            Console.WriteLine("Pearson Correlation: " + pearsons + " :: " + valuation);
            Console.ReadLine();
        }

        private static double GetMean(double[] data)
        {
            double sum = data[0];

            for (int i = 1; i < data.Length; i++) {
                sum += data[i];
            }

            return sum / data.Length;
        }

        private static double GetMedian(double[] data)
        {
            
            if (data.Length % 2 != 0)
            {
                return data[data.Length / 2];
            }
            else
            {
                return (data[data.Length / 2] + data[(data.Length / 2) + 1]) / 2;
            }
        }

        private static double GetZScore(double[] data, double mean, double standardDeviation)
        {
            Random r = new Random();

            int randomSelection = r.Next(data.Length);
            return (data[randomSelection] - mean) / standardDeviation;
        }

        private static double CalculateStandardDeviationP(double[] data, double mean)
        {
            double sum = 0;

            foreach (var t in data)
            {
                sum += Math.Pow(mean - t, 2);
            }
            
            return Math.Sqrt(sum / data.Length);
        }

        private static double PearsonCorrelation(double[] data1, double[] data2, double mean1, double mean2, double sd1,
            double sd2)
        {
            double sum = 0.0;

            double squareXSum = 0;
            double squareYSum = 0;

            for (int i = 0; i < data1.Length; i++)
            {
                sum += (data1[i] - mean1) * (data2[i] - mean2);
                squareXSum += Math.Pow(data[i] - mean1, 2);
                squareYSum += Math.Pow(data2[i] - mean2, 2);
            }

            return sum / Math.Sqrt(squareXSum * squareYSum);
        }
    }
}
