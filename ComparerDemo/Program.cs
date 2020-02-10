using C;
using System;
using System.Collections.Generic;

namespace ComparerDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			// create a new hashset that uses ordered collection comparisons
			var set = new HashSet<int[]>(OrderedCollectionEqualityComparer<int>.Default);
			// add a collection:
			set.Add(new int[] { 1, 2, 3, 4, 5 });

			// true:
			Console.WriteLine("Ordered cmp - contains 1-5: " + set.Contains(new int[] { 1, 2, 3, 4, 5 }));
			// false:
			Console.WriteLine("Ordered cmp contains 5-1: " + set.Contains(new int[] { 5, 4, 3, 2, 1 }));

			// create a new hashset that uses unordered collection comparisons
			set = new HashSet<int[]>(UnorderedCollectionEqualityComparer<int>.Default);
			set.Add(new int[] { 1, 2, 3, 4, 5 });

			// true:
			Console.WriteLine("Unordered cmp - contains 1-5: " + set.Contains(new int[] { 1, 2, 3, 4, 5 }));
			// true:
			Console.WriteLine("Unordered cmp contains 5-1: " + set.Contains(new int[] { 5, 4, 3, 2, 1 }));

		}
	}
}
