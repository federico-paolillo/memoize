using System;
using System.Collections.Generic;

namespace Memoization.Arguments
{
	/// <summary>
	/// Stores and retrieves IEqualityComparers for different types.
	/// </summary>
	internal class ComparersStore
	{
		private readonly Dictionary<Type, IEqualityComparerWrapper> comparerWrappers = new Dictionary<Type, IEqualityComparerWrapper>();

		/// <summary>
		/// Registers in the Store for later use the IEqualityComparer specified.
		/// </summary>
		public void Add<T>(IEqualityComparer<T> comparer)
		{
			if (comparer == null) throw new ArgumentNullException(nameof(comparer), "An IEqualityComparer instance is required.");

			var typeKey = typeof(T);

			var comparerWrapper = new EqualityComparerWrapper<T>(comparer);

			//Adds or replaces the old comparer
			comparerWrappers[typeKey] = comparerWrapper;
		}

		/// <summary>
		/// Returns the IEqualityComparer instance for the type specified or default comparer if none was registered.
		/// </summary>
		public IEqualityComparer<T> Get<T>()
		{
			var typeKey = typeof(T);

			bool foundSomething = comparerWrappers.TryGetValue(typeKey, out IEqualityComparerWrapper maybeWrapper);

			var maybeWrapperForType = maybeWrapper as EqualityComparerWrapper<T>;

			if (maybeWrapperForType == null) return EqualityComparer<T>.Default;

			return maybeWrapperForType.Comparer;
		}
	}
}
