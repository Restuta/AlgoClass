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
            int[] inputArray = File.ReadAllLines("IntegerArray.txt").Select(x => int.Parse(x)).ToArray();

            //var inputArray = Enumerable.Range(0, 6).Reverse().ToArray();
            //var inputArray = new int[] { 3, 2, 5, 8, 4, 1 };

            var inversions = CountInversions(inputArray);
            Console.WriteLine(inversions);
        }

        private static long CountInversions(int[] inputArray)
        {
            if (inputArray.Length == 1)
                return 0;

            int firstHalf = (inputArray.Length) / 2;
            var left = inputArray.Take(firstHalf).ToArray();
            var right = inputArray.Skip(firstHalf).Take(inputArray.Length - firstHalf).ToArray();

            long x = CountInversions(left);
            long y = CountInversions(right);
            long z = CountSplitInversions(left, right, inputArray);

            return x + y + z;
        }

        private static long CountSplitInversions(int[] left, int[] right, int[] inputArray)
        {
            long numberOfInversions = 0;
            int i = 0;
            int j = 0;

            int k = 0;
            while (i < left.Length || j < right.Length)
            {
                if (AllElementsWereCopiedFrom(right, j) || (i < left.Length && left[i] < right[j]))
                {
                    inputArray[k] = left[i];
                    i++;
                }
                else if (AllElementsWereCopiedFrom(left, i) || (j < right.Length && left[i] > right[j]))
                {
                    inputArray[k] = right[j];
                    j++;

                    numberOfInversions += left.Length - i;
                }
                k++;
            }

            return numberOfInversions;
        }

        private static bool AllElementsWereCopiedFrom(int[] array, int i)
        {
            return i >= array.Length;
        }
    }
}
