using System;

namespace FuncUtils.Memoization
{
	/// <summary>
	/// Provides infrastructure to function memoization.
	/// Weakly cachee results and arguments of a function call.
	/// </summary>
	public abstract partial class Memoizer
	{
		private object lastResult = null;
		private object[] lastArgs = null;

		/// <summary>
		/// Checks if the last recorded arguments stored match the current arguments given.
		/// </summary>
		/// <param name="currentArgs">Args of the current call to check against the last recorded parameters.</param>
		/// <remarks> Make sure to pass the parameters in the same order as they are given to the Func.</remarks>
		/// <returns><code>true</code> if the current arguments equals the last recorded arguments.</returns>
		protected bool ArgsMatchPreviousCall(params object[] currentArgs)
		{
			if (lastArgs == null) return false;
			if (lastArgs.Length != currentArgs.Length) throw new InvalidOperationException("There was a different amount of arguments stored sinc last time than what was supplied now.");

			for (int i = 0; i < currentArgs.Length; i++)
				if (!AreEqual(currentArgs[i], lastArgs[i])) return false;

			return true;
		}

		/// <summary>
		/// Retrieves the last result stored and casts it to the type specified.
		/// If there is not a last result stored an Exception is thrown
		/// </summary>
		protected TResult LastResult<TResult>() => (TResult)lastResult;

		/// <summary>
		/// Saves the results specified on this instance.
		/// Watch out for mem. leaks.
		/// </summary>
		protected void StoreResult(object result) => lastResult = result;

		/// <summary>
		/// Stores the array specified on this instance last arguments.
		/// The array specified will be used to decide if to call or not the memoized function.
		/// Watch out for mem. leaks.
		/// </summary>
		protected void StoreArguments(params object[] currentArgs) => lastArgs = currentArgs.Clone() as object[];

		/// <summary>
		/// Checks if the two arguments are Equal or not
		/// </summary>
		private bool AreEqual(object currentArg, object lastArg)
		{
			//Are they the same thing ?
			if (object.ReferenceEquals(currentArg, lastArg)) return true;

			//Standard Equal comparison, will handle if left or right is null
			return object.Equals(currentArg, lastArg);
		}

		/// <summary>
		/// Forgets anything that was memoized
		/// </summary>
		public void Reset()
		{
			lastArgs = null;
			lastResult = null;
		}
	}
}
