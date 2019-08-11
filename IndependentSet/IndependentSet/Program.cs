using System;
using System.Collections.Generic;


// A max weight of indepentd set (IS) problem for learning Dynamic Programming
namespace IndependentSet
{
    class IndependentSet
    {
        public static void Main(string[] args)
        {
            int[] pathGraph = { 1, 4, 5, 4 };
            int[] maxWeightIS = new int[pathGraph.Length + 1];
            int i;

            // Initialization
            maxWeightIS[0] = 0;
            maxWeightIS[1] = pathGraph[0];

            // Solve sub-problem sets.
            for (i = 2; i <= pathGraph.Length; i++)
            {
                maxWeightIS[i] = Math.Max(maxWeightIS[i - 1], maxWeightIS[i - 2] + pathGraph[i - 1]);
            }

            // Reconstruction of the nodes composing the IS.
            List<int> ISNodeList = new List<int>();
            i = maxWeightIS.Length - 1;
            while( i > 1 )
            {
                if ((maxWeightIS[i - 2] + pathGraph[i - 1]) > maxWeightIS[i - 1])
                {
                    ISNodeList.Add(i - 1);
                    i -= 2;
                }
                else
                    i--;
            }

            // Result
            Console.WriteLine("Max weight of IS = {0}", maxWeightIS[maxWeightIS.Length - 1]);
            for (i = 0; i < ISNodeList.Count; i++)
            {
                Console.WriteLine("pathGraph[{0}] = {1}", ISNodeList[i], pathGraph[ISNodeList[i]]);
            }
        }
    }
}
