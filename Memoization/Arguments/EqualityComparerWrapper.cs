using System;
using System.Collections.Generic;

namespace Memoization.Arguments
{
	/// <summary>
	/// Marker interface.
	/// Artificially creates a non-generic type for an IEqualityComparer so that it is possible to store comparers for different types together. 
	/// </summary>
	internal interface IEqualityComparerWrapper
	{
		bool Equals(object x, object y);
	}

	/// <summary>
	/// Wraps an IEqualityComparer of T and marks it with IEqualityComparerWrapper marker interface.
	/// This class make it possible to store IEqualityComparers for different types together.
	/// </summary>
	internal sealed class EqualityComparerWrapper<T> : IEqualityComparerWrapper
	{
		public IEqualityComparer<T> Comparer { get; }

		public EqualityComparerWrapper(IEqualityComparer<T> equalityComparer)
		{
			Comparer = equalityComparer ?? throw new ArgumentNullException(nameof(equalityComparer));
		}

		bool IEqualityComparerWrapper.Equals(object x, object y) => Comparer.Equals((T)x, (T)y);
	}
}
