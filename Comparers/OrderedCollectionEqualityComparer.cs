using System;
using System.Collections.Generic;
using System.Text;

namespace C
{
	/// <summary>
	/// Provides ordered comparisons of collections
	/// </summary>
	/// <typeparam name="T">The element type of the collections to compare</typeparam>
#if CMPLIB
	public
#endif
	class OrderedCollectionEqualityComparer<T> : IEqualityComparer<IList<T>>, IEqualityComparer<ICollection<T>>
	{
		/// <summary>
		/// Provides the default instance of this comparer
		/// </summary>
		public static readonly OrderedCollectionEqualityComparer<T> Default = new OrderedCollectionEqualityComparer<T>();
		private OrderedCollectionEqualityComparer()
		{

		}
		bool IEqualityComparer<IList<T>>.Equals(IList<T> x, IList<T> y)
			=>Equals(x, y);
		
		int IEqualityComparer<IList<T>>.GetHashCode(IList<T> x)
		{
			var result = 0;
			if (null == x)
				return result;
			for(int ic=x.Count,i=0;i<ic;++i)
			{
				var xx = x[i];
				if (null != xx)
					result ^= xx.GetHashCode();
			}
			return result;
		}
		bool IEqualityComparer<ICollection<T>>.Equals(ICollection<T> x, ICollection<T> y)
			=> Equals(x, y);
		int IEqualityComparer<ICollection<T>>.GetHashCode(ICollection<T> x)
		{
			var result = 0;
			if (null == x)
				return result;
			foreach(var xx in x)
			{
				if (null != xx)
					result ^= xx.GetHashCode();
			}
			return result;
		}
		/// <summary>
		/// Indicates whether or not two lists are equal
		/// </summary>
		/// <param name="x">The first list to compare</param>
		/// <param name="y">The second list to compare</param>
		/// <returns>True if both lists contain the same items in the same order, otherwise false</returns>
		public static bool Equals(IList<T> x,IList<T> y)
		{
			if (ReferenceEquals(x, y)) 
				return true;
			else if (ReferenceEquals(null,x) || ReferenceEquals(null,y)) 
				return false;
			var ic = x.Count;
			if (ic != y.Count) return false;
			for(var i = 0;i<ic;++i)
			{
				if(!Equals(x[i],y[i]))
					return false;
			}
			return true;
		}
		/// <summary>
		/// Indicates whether or not two collections are equal
		/// </summary>
		/// <param name="x">The first collection to compare</param>
		/// <param name="y">The second collection to compare</param>
		/// <returns>True if both lists contain the same items in the same order, otherwise false</returns>
		public static bool Equals(ICollection<T> x, ICollection<T> y)
		{
			if (ReferenceEquals(x, y))
				return true;
			else if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
				return false;
			if (x.Count != y.Count) return false;
			using(var xe = x.GetEnumerator())
			{
				using(var ye=y.GetEnumerator())
				{
					while(xe.MoveNext() && ye.MoveNext())
					{
						if (!Equals(xe.Current, ye.Current))
							return false;
					}
				}
			}
			return true;
		}
	}
}
