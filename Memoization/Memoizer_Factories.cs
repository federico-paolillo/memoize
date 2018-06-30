using System;

namespace Memoization
{
	public abstract partial class Memoizer
	{
		/// <summary>
		/// Returns an instance of <see cref="Memoizer{T1, TOut}"/> that memoizes the <see cref="Func{T, TResult}"/> given.
		/// </summary>
		/// <typeparam name="T1">The type of the parameter of the <see cref="Func{T, TResult}"/> given.</typeparam>
		/// <typeparam name="TOut">The type of the return value of the <see cref="Func{T, TResult}"/> given.</typeparam>
		/// <param name="func"><see cref="Func{T, TResult}"/> to memoize.</param>
		/// <returns>An instance of a <see cref="Memoizer{T1, TOut}"/> that memoizes the Func given.</returns>
		public static Memoizer<T1, TOut> Memoize<T1, TOut>(Func<T1, TOut> func) => new Memoizer<T1,  TOut>(func);

		/// <summary>
		/// Returns an instance of <see cref="Memoizer{T1, T2, TOut}"/> that memoizes the <see cref="Func{T1, T2, TResult}"/> given.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter of the <see cref="Func{T1, T2, TResult}"/> given.</typeparam>
		/// <typeparam name="T2">The type of the second parameter of the <see cref="Func{T1, T2, TResult}"/> given.</typeparam>
		/// <typeparam name="TOut">The type of the return value of the <see cref="Func{T1, T2, TResult}"/> given.</typeparam>
		/// <param name="func"><see cref="Func{T1, T2, TResult}"/> to memoize.</param>
		/// <returns>An instance of a <see cref="Memoizer{T1, T2, TOut}"/> that memoizes the Func given.</returns>
		public static Memoizer<T1, T2, TOut> Memoize<T1, T2, TOut>(Func<T1, T2, TOut> func) => new Memoizer<T1, T2, TOut>(func);
	}
}
