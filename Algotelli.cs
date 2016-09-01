using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGA.Classes
{
    class Algotelli
    {
        // Turn raw information into encoded data in Patterns.csv

        // Algotelli's main function.
        public static void algoT()
        {
            Console.WriteLine("Algotelli - Normalizing Data to Numerical values");

            // rawData is our raw data that we will be normalizing to numerical values.
            string[] rawData = new string[] {
                "TCP 55425 test 83 other",
                "TCP 55455 test 49155 other",
                "TCP 55460 test 19724 other",
                "TCP 55467 test 19724 other",
                "TCP 55918 test 83 other",
                "TCP 55920 test 443 good",
                "TCP 55921 test 80 bad",
                "TCP 55922 test 80 bad",
                "TCP 55923 test 443 good",
                "TCP 55924 test 443 good",
                "TCP 55925 test 443 good",
                "TCP 55931 test 443 other",
                "UDP 7337 test 55431 other",
                "UDP 7337 test 55469 other",
                "UDP 7337 test 55879 other",
                "UDP 55431 test 7337 other"
            };
            Console.WriteLine("\nRaw Data is: \n");
            ShowRaw(rawData); // Show our rawData for beta and testing purposes.

            string[] colTypes = new string[5] {
                "binary",
                "numeric",
                "categorical",
                "numeric",
                "categorical"
            };
            Console.WriteLine("\nColumn types are:\n");
            ShowCol(colTypes);

            Console.WriteLine("\nBegin transform");
            double[][] nnData = Transform(rawData, colTypes);

            Console.WriteLine("\nTransform complete");
            Console.WriteLine("\nEncoded and normalized result data is:\n");
            ShowTransformed(nnData);
        }

        static double[][] Transform(string[] rawData, string[] colTypes)
        {
            // tokenize raw data into a string matrix
            string[][] data = LoadData(rawData);
            Console.WriteLine("\nTokenized data:\n");
            ShowToken(data);

            Console.WriteLine("\nScanning tokenized data to extract distinct values");
            string[][] distinctValues = GetValues(data, colTypes);  // includes binary
            Console.WriteLine("\nDistinct values:\n");
            ShowDistinctValues(distinctValues);

            Console.WriteLine("\nComputing number of columns for result matrix");
            int extraCols = NumNewCols(distinctValues, colTypes); // binary does not add any
            Console.WriteLine("Adding " + extraCols + " columns for categorical data encoding");

            double[][] result = new double[data.Length][];
            for (int i = 0; i < result.Length; ++i)
                result[i] = new double[data[0].Length + extraCols];

            Console.WriteLine("\nComputing means and standard deviations of numeric data");
            double[] means = GetMeans(data, colTypes);
            double[] stdDevs = GetStdDevs(data, colTypes, means);
            Console.WriteLine("\nMeans:");
            ShowVector(means, 2);
            Console.WriteLine("\nStandard deviations:");
            ShowVector(stdDevs, 2);

            Console.WriteLine("\nEntering main transform loop");
            for (int row = 0; row < data.Length; ++row)
            {
                int k = 0;  // walk across result cols
                for (int col = 0; col < data[row].Length; ++col)
                {
                    string val = data[row][col];
                    bool isBinary = (colTypes[col] == "binary");
                    bool isCategorical = (colTypes[col] == "categorical");
                    bool isNumeric = (colTypes[col] == "numeric");
                    bool isIndependent = (col < data[0].Length - 1);
                    bool isDependent = (col == data[0].Length - 1);

                    // binary x value -> -1.0 or +1.0
                    if (isBinary && isIndependent)
                    {
                        result[row][k++] = BinaryIndepenToValue(val, col, distinctValues);
                    }
                    // binary y value -> 0.0 or 1.0
                    else if (isBinary && isDependent)
                    {
                        result[row][k] = BinaryDepenToValue(val, col, distinctValues);  // no k++
                    }
                    // cat x value -> [0.0, 1.0, 1.0] or [-1.0, -1.0, -1.0]
                    else if (isCategorical && isIndependent)
                    {
                        double[] vals = CatIndepenToValues(val, col, distinctValues);
                        for (int j = 0; j < vals.Length; ++j)
                            result[row][k++] = vals[j];
                    }
                    // cat y value -> [1.0, 0.0, 0.0]
                    else if (isCategorical && isDependent)
                    {
                        double[] vals = CatDepenToValues(val, col, distinctValues);
                        for (int j = 0; j < vals.Length; ++j)
                            result[row][k++] = vals[j];
                    }
                    else if (isNumeric && isIndependent)
                    {
                        result[row][k++] = NumIndepenToValue(val, col, means, stdDevs);
                    }
                    else if (isNumeric && isDependent)
                    {
                        result[row][k] = double.Parse(val); // no k++
                    }
                }
            }
            return result;
        }

        static string[][] LoadData(string[] rawData)
        {
            int numRows = rawData.Length;
            int numCols = rawData[0].Split(' ').Length;
            string[][] result = new string[numRows][];

            for (int i = 0; i < numRows; ++i)
            {
                result[i] = new string[numCols];
                string[] tokens = rawData[i].Split(' ');
                Array.Copy(tokens, result[i], numCols);
            }
            return result;
        }

        #region Transform Functions
        // binary x value -> -1 or +1
        static double BinaryIndepenToValue(string val, int col, string[][] distinctValues)
        {
            if (distinctValues[col].Length != 2)
                throw new Exception("Binary x data only 2 values allowed");
            if (distinctValues[col][0] == val)
                return -1.0;
            else
                return +1.0;
        }

        // binary y value -> 0 or 1
        static double BinaryDepenToValue(string val, int col, string[][] distinctValues)
        {
            if (distinctValues[col].Length != 2)
                throw new Exception("Binary y data only 2 values allowed");
            if (distinctValues[col][0] == val)
                return 0.0;
            else
                return 1.0;
        }

        // categorical x value -> 1-of-(C-1) effects encoding
        static double[] CatIndepenToValues(string val, int col, string[][] distinctValues)
        {
            if (distinctValues[col].Length == 2)
                throw new Exception("Categorical x data only 1, 3+ values allowed");
            int size = distinctValues[col].Length;
            double[] result = new double[size];

            int idx = 0;
            for (int i = 0; i < size; ++i)
            {
                if (distinctValues[col][i] == val)
                {
                    idx = i; break;
                }
            }

            if (idx == size - 1) // the value is the last one so use effects encoding
            {
                for (int i = 0; i < size; ++i) // ex: [-1.0, -1.0, -1.0]
                {
                    result[i] = -1.0;
                }
            }
            else // value is not last, use dummy
            {
                result[result.Length - 1 - idx] = +1.0; // ex: [0.0, 1.0, 0.0]
            }
            return result;
        }

        // categorical y value -> 1-of-C dummy encoding
        static double[] CatDepenToValues(string val, int col, string[][] distinctValues)
        {
            if (distinctValues[col].Length == 2)
                throw new Exception("Categorical x data only 1, 3+ values allowed");
            int size = distinctValues[col].Length;
            double[] result = new double[size];

            int idx = 0;
            for (int i = 0; i < size; ++i)
            {
                if (distinctValues[col][i] == val)
                {
                    idx = i; break;
                }
            }
            result[result.Length - 1 - idx] = 1.0; // ex: [0.0, 1.0, 0.0]
            return result;
        }

        // numeric x value -> (x - m) / s
        static double NumIndepenToValue(string val, int col, double[] means, double[] stdDevs)
        {
            double x = double.Parse(val);
            double m = means[col];
            double sd = stdDevs[col];
            return (x - m) / sd;
        }

        static int NumNewCols(string[][] distinctValues, string[] colTypes)
        {
            // number of additional columns needed due to categorical encoding
            int result = 0;
            for (int i = 0; i < colTypes.Length; ++i)
            {
                if (colTypes[i] == "categorical")
                {
                    int numCatValues = distinctValues[i].Length;
                    result += (numCatValues - 1);
                }
            }
            return result;
        }
        #endregion

        #region Show Data Functions
        static void ShowRaw(string[] rawData)
        {
            for (int i = 0; i < rawData.Length; ++i)
                Console.WriteLine(rawData[i]);
        }

        static void ShowCol(string[] colTypes)
        {
            for (int i = 0; i < colTypes.Length; ++i)
                Console.WriteLine(colTypes[i] + "  ");
            Console.WriteLine("");
        }

        static void ShowToken(string[][] data)
        {
            Console.WriteLine("Protocol   Size       test      Port         -> Result");
            Console.WriteLine("--------------------------------------------------------------");

            for (int i = 0; i < data.Length; ++i)
            {
                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (j == 4) Console.Write(" -> ");
                    Console.Write(data[i][j].PadRight(10) + " ");
                }
                Console.WriteLine("");
            }
        }

        static void ShowDistinctValues(string[][] distinctValues)
        {
            for (int i = 0; i < distinctValues.Length; ++i)
            {
                Console.Write("[" + i + "] ");
                for (int j = 0; j < distinctValues[i].Length; ++j)
                {
                    Console.Write(distinctValues[i][j] + "  ");
                }
                Console.WriteLine("");
            }
        }

        public static void ShowVector(double[] vector, int decimals)
        {
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i > 0 && i % 12 == 0) // max of 12 values per row
                    Console.WriteLine("");
                if (vector[i] >= 0.0) Console.Write(" ");
                Console.Write(vector[i].ToString("F" + decimals) + " "); // 2 decimals
            }
            Console.WriteLine("\n");
        }

        static void ShowTransformed(double[][] nnData)
        {
            Console.WriteLine("Protocol            Size         test    Port   -> Result");
            Console.Write("------------------------------------------------------------");
            Console.WriteLine("--------------------");
            for (int i = 0; i < nnData.Length; ++i)
            {
                for (int j = 0; j < nnData[i].Length; ++j)
                {
                    if (j == 6) Console.Write("-> ");
                    if (nnData[i][j] >= 0.0) Console.Write(" ");
                    Console.Write(nnData[i][j].ToString("F2") + "   ");
                }
                Console.WriteLine("");
            }
        }
        #endregion

        #region Get Data Functions
        static string[][] GetValues(string[][] data, string[] colTypes)
        {
            // examine tokenized data to get distinct values for cat and binary columns
            int numCols = data[0].Length;
            string[][] result = new string[numCols][];
            for (int col = 0; col < numCols; ++col)
            {
                if (colTypes[col] == "numeric")
                {
                    result[col] = new string[] { "(numeric)" };
                }
                else
                {
                    Dictionary<string, bool> d = new Dictionary<string, bool>();    // bool is a dummy
                    for (int row = 0; row < data.Length; ++row)
                    {
                        string currVal = data[row][col];
                        if (d.ContainsKey(currVal) == false)
                            d.Add(currVal, true);
                    }
                    result[col] = new string[d.Count];
                    int k = 0;
                    foreach (string val in d.Keys)
                        result[col][k++] = val;
                }
            }
            return result;
        }

        static double[] GetMeans(string[][] data, string[] colTypes)
        {
            double[] result = new double[data.Length];
            for (int col = 0; col < data[0].Length; ++col)  // each column
            {
                if (colTypes[col] != "numeric") continue;   // curr col is not numeric

                double sum = 0.0;
                for (int row = 0; row < data.Length; ++row)
                {
                    double val = double.Parse(data[row][col]);
                    sum += val;
                }
                result[col] = sum / data.Length;
            }
            return result;
        }

        static double[] GetStdDevs(string[][] data, string[] colTypes, double[] means)
        {
            double[] result = new double[data.Length];
            for (int col = 0; col < data[0].Length; ++col) // each column
            {
                if (colTypes[col] != "numeric") continue; // curr col is not numeric

                double sum = 0.0;
                for (int row = 0; row < data.Length; ++row)
                {
                    double val = double.Parse(data[row][col]);
                    sum += (val - means[col]) * (val - means[col]);
                }
                result[col] = Math.Sqrt(sum / data.Length);
            }
            return result;
        }
        #endregion
    }
}
