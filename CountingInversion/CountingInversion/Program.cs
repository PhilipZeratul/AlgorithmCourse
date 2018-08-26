using System;
using System.IO;


namespace CountingInversion
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //int[] input = new int[] { 9, 12, 3, 1, 6, 8, 2, 5, 14, 13, 11, 7, 10, 4, 0 };
            //int[] input = new int[] { 37, 7, 2, 14, 35, 47, 10, 24, 44, 17, 34, 11, 16, 48, 1, 39, 6, 33, 43, 26, 40, 4, 28, 5, 38, 41, 42, 12, 13, 21, 29, 18, 3, 19, 0, 32, 46, 27, 31, 25, 15, 36, 20, 8, 9, 49, 22, 23, 30, 45 };
            int[] input = ReadInputFromFile();

            long invCount = SortAndCount(ref input);

            Console.WriteLine("Total inversion count = {0}", invCount);
        }

        private static int[] ReadInputFromFile()
        {
            StreamReader file = new StreamReader(@"/Users/phillipzhang/Work/git/AlgorithmCourse/CountingInversion/InputArray.txt");

            string line;
            int counter = 0;
            int[] input = new int[100000]; 

            while((line = file.ReadLine()) != null)  
            {
                input[counter] = Int32.Parse(line);
                counter++;  
            }

            file.Close();
            Console.WriteLine("Read Input Complete");
            return input;
        }

        private static long SortAndCount(ref int[] source)
        {
            if (source.Length == 1)
            {
                return 0;
            }

            int halfLength = source.Length / 2;
            int[] firstHalf = new int[halfLength];
            int[] secondHalf = new int[source.Length - halfLength];

            for (int i = 0; i < halfLength; i++)
            {
                firstHalf[i] = source[i];
            }
            for (int i = 0; i < source.Length - halfLength; i++)
            {
                secondHalf[i] = source[i + halfLength];
            }

            long x = SortAndCount(ref firstHalf);
            long y = SortAndCount(ref secondHalf);
            long z = MergeAndCountSplitInv(firstHalf, secondHalf, ref source);

            return x + y + z;
        }

        private static long MergeAndCountSplitInv(int[] firstHalf, int[] secondHalf, ref int[] source)
        {
            int[] merge = new int[firstHalf.Length + secondHalf.Length];

            long invCount = 0;
            int i = 0, j = 0;
            for (int k = 0; k < merge.Length; k++)
            {
                if (i >= firstHalf.Length)
                {
                    merge[k] = secondHalf[j];
                    j++;
                    continue;
                }

                if (j >= secondHalf.Length)
                {
                    merge[k] = firstHalf[i];
                    i++;
                    continue;
                }


                if (firstHalf[i] <= secondHalf[j])
                {
                    merge[k] = firstHalf[i];
                    i++;
                }
                else
                {
                    merge[k] = secondHalf[j];
                    j++;
                    invCount += firstHalf.Length - i;
                }
            }
            source = merge;


            //for (int x = 0; x < firstHalf.Length; x++)
            //{
            //    Console.WriteLine("firstHalf[{0}] = {1}", x, firstHalf[x]);
            //}
            //for (int x = 0; x < secondHalf.Length; x++)
            //{
            //    Console.WriteLine("secondHalf[{0}] = {1}", x, secondHalf[x]);
            //}
            //for (int x = 0; x < merge.Length; x++)
            //{
            //    Console.WriteLine("merge[{0}] = {1}", x, merge[x]);
            //}

            //Console.WriteLine("invCount = {0}", invCount);

            return invCount;
        }
    }
}
