using System;

namespace Memoization
{
	/// <summary>
	/// A memoized Func with 7 arguments
	/// </summary>
	public sealed class Memoizer<T1, T2, T3, T4, T5, T6, T7, TOut> : Memoizer
	{
		private readonly Func<T1, T2, T3, T4, T5, T6, T7, TOut> func = null;

		internal Memoizer(Func<T1, T2, T3, T4, T5, T6, T7, TOut> func)
		{
			this.func = func ?? throw new ArgumentNullException(nameof(func), "A Func delegate to memoize is required.");
		}

		public TOut Call(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			if (ArgsMatchPreviousCall(arg1, arg2, arg3, arg4, arg5, arg6, arg7)) return LastResult<TOut>();

			TOut result = func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

			StoreArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			StoreResult(result);

			return result;
		}

		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, TOut>(Memoizer<T1, T2, T3, T4, T5, T6, T7, TOut> fn) => fn.Call;
	}
}
