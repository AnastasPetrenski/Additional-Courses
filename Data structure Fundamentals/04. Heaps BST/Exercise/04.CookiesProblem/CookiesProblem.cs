using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bagOrderedDescending = new OrderedBag<int>(ComparerElement);

            var bag = new OrderedBag<int>();

            foreach (var item in cookies)
            {
                bag.Add(item);
            }

            var currentSweetness = bag.GetFirst();

            if (currentSweetness < k)
            {
                return GetSweetnessOperations(k, bag, currentSweetness);
            }

            return bagOrderedDescending.Count;

        }

        private int GetSweetnessOperations(int k, OrderedBag<int> bag, int currentSweetness)
        {
            int count = 0;
            
            while (bag.Count > 1 && currentSweetness < k)
            {
                var firstElement = bag.RemoveFirst();
                var secondElement = bag.RemoveFirst();

                var combined = firstElement + (2 * secondElement);
                bag.Add(combined);
                count++;

                currentSweetness = bag.GetFirst();
            }

            return currentSweetness < k ? -1 : count;
        }

        private int ComparerElement(int first, int second)
        {
            return second - first;
        }
    }
}
