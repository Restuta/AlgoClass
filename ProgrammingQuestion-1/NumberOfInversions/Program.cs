using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NumberOfInversions
{
    class Program
    {
        static void Main(string[] args)
        {
            //2407905288
            //int[] inputArray = File.ReadAllLines("IntegerArray.txt").Select(x => int.Parse(x)).ToArray();

            //var inputArray = Enumerable.Range(0, 6).Reverse().ToArray();
            var inputArray = new int[]{3, 2, 5, 8, 4, 1};

            var inversions = CountInversions(inputArray);
            Console.WriteLine(inversions);
        }

        private static long CountInversions(int[] inputArray)
        {
            if (inputArray.Length == 1)
                return 0;

            //if (inputArray.Length == 2)
            //{
            //    if (inputArray[0] > inputArray[1])
            //    {
            //        int a = inputArray[0];
            //        inputArray[0] = inputArray[1];
            //        inputArray[1] = a;
            //    }
            //}

            int firstHalf = (inputArray.Length) / 2;
            var left = inputArray.Take(firstHalf).ToArray();
            var right = inputArray.Skip(firstHalf).Take(inputArray.Length - firstHalf).ToArray();

            long x = CountInversions(left);
            long y = CountInversions(right);
            //Array.Sort(left);
            //Array.Sort(right);
            long z = CountSplitInversions(left, right, inputArray);

            return x + y + z;
        }

        private static long CountSplitInversions(int[] left, int[] right, int[] inputArray)
        {
            if (left.Length == 1 && right.Length == 1)
                return left[0] > right[0] ? 1 : 0;

            long numberOfInversions = 0;
            int i = 0;
            int j = 0;

            for (int k = 0; k < left.Length + right.Length; k++)
            {
                if (left[i] < right[j])
                {
                    if (i < left.Length - 1)
                    {
                        i++;
                    }
                }
                else if(left[i] > right[j])
                {
                    if (j != right.Length - 1)
                    {
                        j++;
                        numberOfInversions += left.Length - i;
                    }
                    else
                    {
                        numberOfInversions += left.Length - i;
                        break;
                    }
                }
            }

            return numberOfInversions;
        }
    }
}
