using System;

namespace FuncUtils.Memoization
{
	public abstract partial class Memoizer
	{
		/// <summary>
		/// Returns the <see cref="Func{T, TResult}"/> given as a memoized function.
		/// </summary>
		/// <typeparam name="T1">The type of the parameter of the <see cref="Func{T, TResult}"/> given.</typeparam>
		/// <typeparam name="TOut">The type of the return value of the <see cref="Func{T, TResult}"/> given.</typeparam>
		/// <param name="func"><see cref="Func{T, TResult}"/> to memoize.</param>
		/// <returns>An instance of a <see cref="Memoizer{T1, TOut}"/> that memoizes the Func given.</returns>
		public static Memoizer<T1, TOut> Memoize<T1, TOut>(Func<T1, TOut> func) => new Memoizer<T1,  TOut>(func);
	}
}
