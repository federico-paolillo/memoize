using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncUtils.Memoization
{
	/// <summary>
	/// Provides infrastructure to Weak function memoization.
	/// Weakly caches results and arguments of a function call.
	/// </summary>
	public abstract class WeakMemoizer
	{
		private NullAwareWeakReference lastResult = null;
		private List<NullAwareWeakReference> lastArgs = null;
		
		/// <summary>
		/// Checks if the last recorded arguments stored match the current arguments given.
		/// </summary>
		/// <param name="currentArgs">Args of the current call to check against the last recorded parameters.</param>
		/// <remarks> Make sure to pass the parameters in the same order as they are given to the Func.</remarks>
		/// <returns><code>true</code> if the current arguments equals the last recorded arguments.</returns>
		protected bool ArgsMatchPreviousCall(params object[] currentArgs)
		{
			if (lastArgs == null) return false;
			if (lastArgs.Count != currentArgs.Length) throw new InvalidOperationException("There was a different amount of arguments stored sinc last time than what was supplied now.");

			CleanupLastArgumentsReferences();

			//If after cleaning up any collected reference we have less arguments than the current call we simply return false.
			if (lastArgs.Count != currentArgs.Length) return false;

			for (int i = 0; i < currentArgs.Length; i++)
				if (!AreEqual(currentArgs[i], lastArgs[i])) return false;

			return true;
		}

		/// <summary>
		/// Retrieves the last result stored and casts it to the type specified.
		/// If there is not a last result stored an Exception is thrown
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <returns></returns>
		protected TResult LastResult<TResult>()
		{
			//TODO: This might bite back, if the caller is unlucky enough that the reference got garbage collected after matching the arguments it will crash.
			if (!lastResult.IsAlive) throw new InvalidOperationException("There is no last result stored.");

			return (TResult)lastResult.Target;
		}

		protected void StoreResult(object result) => lastResult = new NullAwareWeakReference(result);

		protected void StoreArguments(params object[] currentArgs)
		{
			List<NullAwareWeakReference> newArgs = new List<NullAwareWeakReference>();

			IEnumerable<NullAwareWeakReference> weakNewArgs = currentArgs.Select(a => new NullAwareWeakReference(a));

			newArgs.AddRange(weakNewArgs);

			lastArgs = newArgs;
		}

		/// <summary>
		/// Removes any garbage collected references to the last arguments used to call this memoized function
		/// </summary>
		private void CleanupLastArgumentsReferences()
		{
			List<NullAwareWeakReference> aliveReferences = new List<NullAwareWeakReference>();
			
			//Keep only the references that are still alive
			foreach (NullAwareWeakReference wkRef in lastArgs)
				if (wkRef.IsAlive) aliveReferences.Add(wkRef);

			lastArgs = aliveReferences;
		}

		private bool AreEqual(object currentArg, NullAwareWeakReference lastArg)
		{
			if (!lastArg.IsAlive) throw new InvalidOperationException("Attempted to compare an argument that was garbage collected.");

			//Are they the same thing ?
			if (object.ReferenceEquals(currentArg, lastArg.Target)) return true;

			//Standard Equal comparison, will handle if left or right is null
			return object.Equals(currentArg, lastArg.Target);
		}
	}
}
