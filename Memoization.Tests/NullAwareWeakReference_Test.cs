using FluentAssertions;
using System;
using Xunit;

namespace FuncUtils.Memoization.Tests
{
	public class NullAwareWeakReference_Test
    {
		[Fact]
		public void Null_marks_the_WeakReference_as_alive()
		{
			//Arrange, Act
			NullAwareWeakReference reference = new NullAwareWeakReference(null);

			//Assert
			reference.IsAlive.Should().BeTrue();
		}

		[Fact]
		public void The_WeakReference_still_dies_if_the_original_object_was_not_a_null_reference()
		{
			//Arrange
			NullAwareWeakReference wkReference = MakeMeWeak();

			//Act
			GC.Collect();

			//Assert
			wkReference.IsAlive.Should().BeFalse();
		}

		/// <summary>
		/// Declares a variable in another scope that will die after the function closes.
		/// </summary>
		private NullAwareWeakReference MakeMeWeak()
		{
			object dummy = new object();

			return new NullAwareWeakReference(dummy);
		}
	}
}
