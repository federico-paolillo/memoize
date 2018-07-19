using System;

namespace Memoization
{
	public abstract partial class Memoizer
	{
		public static Memoizer<T1, TOut> Memoize<T1, TOut>(Func<T1, TOut> func) => new Memoizer<T1,  TOut>(func);

		public static Memoizer<T1, T2, TOut> Memoize<T1, T2, TOut>(Func<T1, T2, TOut> func) => new Memoizer<T1, T2, TOut>(func);

		public static Memoizer<T1, T2, T3, TOut> Memoize<T1, T2, T3, TOut>(Func<T1, T2, T3, TOut> func) => new Memoizer<T1, T2, T3, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, TOut> Memoize<T1, T2, T3, T4, TOut>(Func<T1, T2, T3, T4, TOut> func) => new Memoizer<T1, T2, T3, T4, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, TOut> Memoize<T1, T2, T3, T4, T5, TOut>(Func<T1, T2, T3, T4, T5, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, TOut> Memoize<T1, T2, T3, T4, T5, T6, TOut>(Func<T1, T2, T3, T4, T5, T6, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(func);

		public static Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut> Memoize<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut> func) => new Memoizer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>(func);
	}
}
