using Moq;
using System;
using Xunit;

namespace Memoization.Tests
{
	public class Memoizer_2_Test
    {
		[Fact]
		public void Call_invokes_original_function_the_first_time()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(2, 3);

			//Assert
			fnSpy.Verify(f => f(2, 3), Times.Once());
		}

		[Fact]
		public void Call_does_not_invoke_original_function_if_the_arguments_are_Equal()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(2, 3);
			memoizedFn.Call(2, 3);
			memoizedFn.Call(2, 3);

			//Assert
			fnSpy.Verify(f => f(2, 3), Times.Once());
		}

		[Fact]
		public void Call_invokes_original_function_when_arguments_changes()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times changing arguments
			memoizedFn.Call(2, 2);
			memoizedFn.Call(2, 2);
			memoizedFn.Call(3, 3);
			memoizedFn.Call(3, 3);

			//Assert
			fnSpy.Verify(f => f(2, 2), Times.Once());
			fnSpy.Verify(f => f(3, 3), Times.Once());
		}

		[Fact]
		public void Memoize_correctly_if_the_argument_is_null()
		{
			//Arrange
			var fnSpy = new Mock<Func<object, object, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(null, string.Empty);
			memoizedFn.Call(null, string.Empty);
			memoizedFn.Call(null, string.Empty);

			//Assert
			fnSpy.Verify(f => f(null, string.Empty), Times.Once());
		}

		[Fact]
		public void Converting_to_Func_implicitly_returns_a_reference_the_Call_method()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			Func<int, int, int> asFunc = memoizedFn;

			//Act
			asFunc.Invoke(10, 11);
			asFunc.Invoke(10, 11);
			asFunc.Invoke(10, 11);

			//Assert
			fnSpy.Verify(f => f(10, 11), Times.Once());
		}

		[Fact]
		public void Calling_reset_will_clear_all_memoized_information()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(1, 2);
			memoizedFn.Call(1, 2);
			memoizedFn.Call(1, 2);

			memoizedFn.Reset();

			memoizedFn.Call(1, 2);

			//Assert
			fnSpy.Verify(f => f(1, 2), Times.Exactly(2));
		}
	}
}
