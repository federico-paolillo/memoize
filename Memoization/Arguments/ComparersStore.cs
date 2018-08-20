using System;
using System.Collections.Generic;
using System.Reflection;

namespace Memoization.Arguments
{
	/// <summary>
	/// Stores and retrieves IEqualityComparers for different types.
	/// </summary>
	internal class ComparersStore
	{
		private readonly Dictionary<Type, IEqualityComparerWrapper> comparerWrappers = new Dictionary<Type, IEqualityComparerWrapper>();

		public static MethodInfo GetMethodInfo { get; } = typeof(ComparersStore).GetTypeInfo().GetDeclaredMethod(nameof(Get));

		/// <summary>
		/// Registers in the Store for later use the IEqualityComparer specified.
		/// </summary>
		public void Add<T>(IEqualityComparer<T> comparer)
		{
			if (comparer == null) throw new ArgumentNullException(nameof(comparer), "An IEqualityComparer instance is required.");

			Type typeKey = typeof(T);

			EqualityComparerWrapper<T> comparerWrapper = new EqualityComparerWrapper<T>(comparer);

			//Adds or replaces the old comparer
			comparerWrappers[typeKey] = comparerWrapper;
		}

		/// <summary>
		/// Returns the IEqualityComparer instance for the type specified or default comparer if none was registered.
		/// </summary>
		public IEqualityComparerWrapper Get<T>()
		{
			Type typeKey = typeof(T);

			bool foundSomething = comparerWrappers.TryGetValue(typeKey, out IEqualityComparerWrapper maybeWrapper);

			EqualityComparerWrapper<T> maybeWrapperForType = maybeWrapper as EqualityComparerWrapper<T>;

			if (maybeWrapperForType == null) return EqualityComparerWrapper<T>.Default;

			return maybeWrapperForType;
		}
	}
}
