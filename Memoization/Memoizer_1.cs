using System;

namespace Memoization
{
	/// <summary>
	/// A memoized Func with 1 argument
	/// </summary>
	public sealed class Memoizer<T1, TOut> : Memoizer
	{
		private readonly Func<T1, TOut> func = null;

		internal Memoizer(Func<T1, TOut> func)
		{
			this.func = func ?? throw new ArgumentNullException(nameof(func), "A Func delegate to memoize is required.");
		}

		public TOut Call(T1 arg1)
		{
			if (ArgsMatchPreviousCall(arg1)) return LastResult<TOut>();

			TOut result = func(arg1);

			StoreArguments(arg1);
			StoreResult(result);

			return result;
		}

		public static implicit operator Func<T1, TOut>(Memoizer<T1, TOut> fn) => fn.Call;
	}
}