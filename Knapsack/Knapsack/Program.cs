using System;
using System.Collections.Generic;


// Dynamic Programming
namespace Knapsack
{
    class MainClass
    {
        private struct Treasure
        {
            public int value;
            public int weight;
        }

        public static void Main(string[] args)
        {
            int knapsackWeight = 6;
            Treasure[] treasures = GetInput();

            // Initialize
            int[][] knapsack = new int[knapsackWeight + 1][];
            for (int x = 0; x < knapsack.Length; x++)
            {
                knapsack[x] = new int[treasures.Length + 1];
                knapsack[x][0] = 0;
            }

            for (int i = 1; i <= treasures.Length; i++) // i: col, current treasure + 1
            {
                for (int x = 0; x < knapsack.Length; x++) // x: row, current weight
                {
                    if (x - treasures[i - 1].weight <= 0)
                        knapsack[x][i] = knapsack[x][i - 1];
                    else
                        knapsack[x][i] = Math.Max(knapsack[x][i - 1], 
                                                  knapsack[x - treasures[i - 1].weight][i - 1] + treasures[i - 1].value);
                }
            }

            // Reconstruction
            int max = 0;
            int current_i = 0;
            int current_x = 0;
            List<int> items = new List<int>();

            for (int i = 1; i <= treasures.Length; i++)
            {
                for (int x = 0; x < knapsack.Length; x++)
                {
                    if (knapsack[x][i] > max)
                    {
                        max = knapsack[x][i];
                        current_i = i;
                        current_x = x;
                    }
                }
            }

            while (current_i > 0)
            {
                if (knapsack[current_x][current_i] > knapsack[current_x][current_i - 1])
                {
                    items.Add(current_i);
                    current_x -= treasures[current_i - 1].weight;
                    current_i--;
                }
                else
                    current_i--;
            }

            // Result
            Console.WriteLine("Max total value = {0}", max);
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine("Treasure {0}, Value {1}, Weight {2}", items[i], treasures[items[i] - 1].value, treasures[items[i] - 1].weight);
            }
        }

        private static Treasure[] GetInput()
        {
            Treasure[] treasures = new Treasure[4];
            treasures[0] = new Treasure { value = 3, weight = 4 };
            treasures[1] = new Treasure { value = 2, weight = 3 };
            treasures[2] = new Treasure { value = 4, weight = 2 };
            treasures[3] = new Treasure { value = 4, weight = 3 };
            return treasures;
        }
    }
}
