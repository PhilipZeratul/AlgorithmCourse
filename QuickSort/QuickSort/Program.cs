using System;
using System.IO;


namespace QuickSort
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[] input = ReadFromFile();
            //int[] input = new int[] { 3, 8, 4, 7, 6, 5, 2, 1, 9, 0 };

            QuickSort quickSort = new QuickSort();
            quickSort.Sort(ref input, 0, input.Length - 1);

            //for (int i = 0; i < input.Length; i++)
            //{
            //    Console.WriteLine("{0}", input[i]);
            //}

            Console.WriteLine("Number of comparisions = {0}", quickSort.comparisions);
        }

        private static int[] ReadFromFile()
        {
            StreamReader file = new StreamReader(@"/Users/phillipzhang/Work/git/AlgorithmCourse/QuickSort/InputArray.txt");

            string line;
            int counter = 0;
            int[] input = new int[10000];

            while ((line = file.ReadLine()) != null)
            {
                input[counter] = Int32.Parse(line);
                counter++;
            }

            file.Close();
            Console.WriteLine("Read Input Complete");
            return input;
        }


        class QuickSort
        {
            public int comparisions = 0;


            public void Sort(ref int[] array, int start, int end)
            {
                if (end - start < 1)
                    return;
                
                comparisions += (end - start);
                Console.WriteLine("start = {0}, end = {1}, comparision = {2}", start, end, comparisions);

                // start is the pivot,
                // i is the split point of less and greater than pivot section.
                PickPivot_3(ref array, start, end);
                
                int i = start + 1;
                for (int j = start + 1; j <= end; j++)
                {
                    if (array[j] < array[start])
                    {
                        Swap(ref array, i, j);
                        i++;
                    }
                }
                Swap(ref array, start, i - 1);
                
                Sort(ref array, start, i - 2);
                Sort(ref array, i, end);
            }
            
            private void PickPivot_1(ref int[] array, int start, int end)
            {
                // Use the start element as pivot, do nothing.
            }

            private void PickPivot_2(ref int[] array, int start, int end)
            {
                // Use the end element as pivot.
                Swap(ref array, start, end);
            }

            private void PickPivot_3(ref int[] array, int start, int end)
            {
                // Use the median element as pivot.
                int middle = (start + end) / 2;
                int[] medianArray = new int[] { array[start], array[middle], array[end] };

                Array.Sort(medianArray);
                int median = start;
                if (medianArray[1] == array[start])
                    median = start;
                else if (medianArray[1] == array[middle])
                    median = middle;
                else if (medianArray[1] == array[end])
                    median = end;
                else
                    throw new System.ArgumentException("Cannot find median");

                Swap(ref array, start, median);
            }
            
            private void Swap(ref int[] array, int first, int second)
            {
                int temp = array[first];
                array[first] = array[second];
                array[second] = temp;
            }
        }
    }
}
