using Moq;
using System;
using Xunit;

namespace Memoization.Tests
{
	public class Memoizer_3_Test
    {
		[Fact]
		public void Call_invokes_original_function_the_first_time()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(2, 3, 4);

			//Assert
			fnSpy.Verify(f => f(2, 3, 4), Times.Once());
		}

		[Fact]
		public void Call_does_not_invoke_original_function_if_the_arguments_are_Equal()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(2, 3, 4);
			memoizedFn.Call(2, 3, 4);
			memoizedFn.Call(2, 3, 4);

			//Assert
			fnSpy.Verify(f => f(2, 3, 4), Times.Once());
		}

		[Fact]
		public void Call_invokes_original_function_when_arguments_changes()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times changing arguments
			memoizedFn.Call(2, 3, 4);
			memoizedFn.Call(2, 3, 4);
			memoizedFn.Call(3, 3, 4);
			memoizedFn.Call(3, 3, 4);

			//Assert
			fnSpy.Verify(f => f(2, 3, 4), Times.Once());
			fnSpy.Verify(f => f(3, 3, 4), Times.Once());
		}

		[Fact]
		public void Memoize_correctly_if_the_argument_is_null()
		{
			//Arrange
			var fnSpy = new Mock<Func<object, object, object, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(null, DBNull.Value, string.Empty);
			memoizedFn.Call(null, DBNull.Value, string.Empty);
			memoizedFn.Call(null, DBNull.Value, string.Empty);

			//Assert
			fnSpy.Verify(f => f(null, DBNull.Value, string.Empty), Times.Once());
		}

		[Fact]
		public void Converting_to_Func_implicitly_returns_a_reference_the_Call_method()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			Func<int, int, int, int> asFunc = memoizedFn;

			//Act
			asFunc.Invoke(10, 11, 3);
			asFunc.Invoke(10, 11, 3);
			asFunc.Invoke(10, 11, 3);

			//Assert
			fnSpy.Verify(f => f(10, 11, 3), Times.Once());
		}

		[Fact]
		public void Calling_reset_will_clear_all_memoized_information()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(1, 2, 3);
			memoizedFn.Call(1, 2, 3);
			memoizedFn.Call(1, 2, 3);

			memoizedFn.Reset();

			memoizedFn.Call(1, 2, 3);

			//Assert
			fnSpy.Verify(f => f(1, 2, 3), Times.Exactly(2));
		}
	}
}
