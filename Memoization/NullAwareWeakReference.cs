using System;

namespace FuncUtils.Memoization
{
	/// <summary>
	/// A WeakReference that treates the value null as a special case.
	/// That is, if the reference is created with a null value, the reference is always alive.
	/// </summary>
	public class NullAwareWeakReference
	{
		private WeakReference wkReference = null;

		public bool IsAlive => wkReference?.IsAlive ?? true;

		public object Target => wkReference?.Target ?? null;

		public NullAwareWeakReference(object reference)
		{
			if (reference != null) wkReference = new WeakReference(reference);
		}
	}
}
