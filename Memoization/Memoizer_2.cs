using System;

namespace Memoization
{
	/// <summary>
	/// A memoized Func with 2 arguments
	/// </summary>
	public sealed class Memoizer<T1, T2, TOut> : Memoizer
	{
		private readonly Func<T1, T2, TOut> func = null;

		internal Memoizer(Func<T1, T2, TOut> func)
		{
			this.func = func ?? throw new ArgumentNullException(nameof(func), "A Func delegate to memoize is required.");
		}

		public TOut Call(T1 arg1, T2 arg2)
		{
			if (ArgsMatchPreviousCall(arg1, arg2)) return PreviousResult<TOut>();

			TOut result = func(arg1, arg2);

			StoreArguments(arg1, arg2);
			StoreResult(result);

			return result;
		}

		public static implicit operator Func<T1, T2, TOut>(Memoizer<T1, T2, TOut> fn) => fn.Call;
	}
}
