using System;

namespace Memoization
{
	/// <summary>
	/// A memoized Func with 3 arguments
	/// </summary>
	public sealed class Memoizer<T1, T2, T3, TOut> : Memoizer
	{
		private readonly Func<T1, T2, T3, TOut> func = null;

		internal Memoizer(Func<T1, T2, T3, TOut> func)
		{
			this.func = func ?? throw new ArgumentNullException(nameof(func), "A Func delegate to memoize is required.");
		}

		public TOut Call(T1 arg1, T2 arg2, T3 arg3)
		{
			if (ArgsMatchPreviousCall(arg1, arg2, arg3)) return LastResult<TOut>();

			TOut result = func(arg1, arg2, arg3);

			StoreArguments(arg1, arg2, arg3);
			StoreResult(result);

			return result;
		}

		public static implicit operator Func<T1, T2, T3, TOut>(Memoizer<T1, T2, T3, TOut> fn) => fn.Call;
	}
}
