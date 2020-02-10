using System;
using System.Collections.Generic;

namespace C
{
	/// <summary>
	/// Provides unordered comparisons of collections
	/// </summary>
	/// <typeparam name="T">The element type of the collections to compare</typeparam>
#if CMPLIB
	public
#endif
	class UnorderedCollectionEqualityComparer<T> : IEqualityComparer<ICollection<T>>,IEqualityComparer<ISet<T>> 
	{
		/// <summary>
		/// Provides the default instance of this comparer
		/// </summary>
		public static readonly UnorderedCollectionEqualityComparer<T> Default = new UnorderedCollectionEqualityComparer<T>();
		private UnorderedCollectionEqualityComparer()
		{

		}
		bool IEqualityComparer<ICollection<T>>.Equals(ICollection<T> x, ICollection<T> y)
			=> Equals(x, y);
		int IEqualityComparer<ICollection<T>>.GetHashCode(ICollection<T> x)
		{
			var result = 0;
			if (null == x) return result;
			foreach(var item in x)
				if(null!=item)
					result ^= item.GetHashCode();
			return result;
		}
		bool IEqualityComparer<ISet<T>>.Equals(ISet<T> x, ISet<T> y)
		{
			return Equals(x, y);
		}
		int IEqualityComparer<ISet<T>>.GetHashCode(ISet<T> x)
		{
			var result = 0;
			if (null == x) return result;
			foreach (var item in x)
				if (null != item)
					result ^= item.GetHashCode();
			return result;
		}
		/// <summary>
		/// Indicates whether or not two collections are equal regardless of order of items
		/// </summary>
		/// <param name="x">The first collection to compare</param>
		/// <param name="y">The second collection to compare</param>
		/// <returns>True if the collections are equal, otherwise false</returns>
		public static bool Equals(ICollection<T> x,ICollection<T> y)
		{
			if (ReferenceEquals(x, y))
				return true;
			else if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
				return false;
			if (x.Count != y.Count)
				return false;
			using (var xe = x.GetEnumerator())
			using (var ye = y.GetEnumerator())
				while (xe.MoveNext() && ye.MoveNext())
					if (!y.Contains(xe.Current) || !x.Contains(ye.Current))
						return false;
			return true;
		}
		/// <summary>
		/// Indicates whether or not two sets are equal regardless of order of items
		/// </summary>
		/// <param name="x">The first set to compare</param>
		/// <param name="y">The second set to compare</param>
		/// <returns>True if the collections are equal, otherwise false</returns>
		public static bool Equals(ISet<T> x, ISet<T> y)
		{
			if (ReferenceEquals(x, y))
				return true;
			else if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
				return false;
			return x.SetEquals(y);
		}
	}
}
