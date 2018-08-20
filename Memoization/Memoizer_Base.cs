using Memoization.Arguments;
using System;

namespace Memoization
{
	/// <summary>
	/// Provides infrastructure to function memoization.
	/// Weakly cachee results and arguments of a function call.
	/// </summary>
	public abstract partial class Memoizer
	{
		private readonly ComparersStore comparersStore = new ComparersStore();

		private object previousResult = null;
		private object[] previousArguments = null;

		/// <summary>
		/// Checks if the last recorded arguments stored match the current arguments given.
		/// </summary>
		/// <param name="currentArguments">Args of the current call to check against the last recorded parameters.</param>
		/// <remarks> Make sure to pass the parameters in the same order as they are given to the Func.</remarks>
		/// <returns><code>true</code> if the current arguments equals the last recorded arguments.</returns>
		protected bool ArgsMatchPreviousCall(params object[] currentArguments)
		{
			if (previousArguments == null) return false;
			if (previousArguments.Length != currentArguments.Length) throw new InvalidOperationException("There was a different number of arguments supplied last time than what was supplied now.");

			for (int i = 0; i < currentArguments.Length; i++)
				if (!AreEqual(currentArguments[i], previousArguments[i])) return false;

			return true;
		}

		/// <summary>
		/// Retrieves the last result stored and casts it to the type specified.
		/// If there is not a last result stored an Exception is thrown.
		/// </summary>
		protected TResult PreviousResult<TResult>()
		{
			return (TResult)previousResult;
		}

		/// <summary>
		/// Saves the results specified on this instance.
		/// Watch out for mem. leaks.
		/// </summary>
		protected void StoreResult(object result)
		{
			previousResult = result;
		}

		/// <summary>
		/// Clones (shallowly) and stores the array specified on this instance last arguments.
		/// The arguments store are used to decide if to call or not the memoized function.
		/// Watch out for mem. leaks. as arguments will live as long as the memoized function.
		/// </summary>
		protected void StoreArguments(params object[] currentArguments)
		{
			previousArguments = currentArguments.Clone() as object[];
		}

		/// <summary>
		/// Checks if the two arguments are Equal or not, optionally using the supplied EqualityComparers
		/// </summary>
		private bool AreEqual(object current, object previous)
		{
			//Are they the same thing ?
			if (object.ReferenceEquals(current, previous)) return true;

			Type currentArgumentValueType = current.GetType();
			Type previousArgumentValueType = previous.GetType();

			if (currentArgumentValueType != previousArgumentValueType) throw new InvalidOperationException("An argument changed type since last time it was supplied.");

			//Fetch a comparer for the arguments
			IEqualityComparerWrapper argumentsComparer = ComparersStore.GetMethodInfo.MakeGenericMethod(currentArgumentValueType).Invoke(comparersStore, parameters: null) as IEqualityComparerWrapper;

			//Compare the arguments			
			return argumentsComparer.Compare(current, previous);
		}

		/// <summary>
		/// Forgets everything that was memoized
		/// </summary>
		public void Reset()
		{
			previousArguments = null;
			previousResult = null;
		}
	}
}
