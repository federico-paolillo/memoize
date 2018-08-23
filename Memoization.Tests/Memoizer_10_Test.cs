using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Memoization.Tests
{
	public class Memoizer_10_Test
    {
        [Fact]
        public void Call_invokes_original_function_the_first_time()
        {
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int, int, int, int, int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

			//Assert
			fnSpy.Verify(f => f(0, 1, 2, 3, 4, 5, 6, 7, 8, 9), Times.Once());
        }

		[Fact]
		public void Call_does_not_invoke_original_function_if_the_arguments_are_Equal()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int, int, int, int, int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

			//Assert
			fnSpy.Verify(f => f(0, 1, 2, 3, 4, 5, 6, 7, 8, 9), Times.Once());
		}

		[Fact]
		public void Call_invokes_original_function_when_arguments_changes()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int, int, int, int, int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times changing arguments
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(10, 11, 12, 13, 14, 15, 16, 17, 18, 19);
			memoizedFn.Call(10, 11, 12, 13, 14, 15, 16, 17, 18, 19);

			//Assert
			fnSpy.Verify(f => f(0, 1, 2, 3, 4, 5, 6, 7, 8, 9), Times.Once());
			fnSpy.Verify(f => f(10, 11, 12, 13, 14, 15, 16, 17, 18, 19), Times.Once());
		}

		[Fact]
		public void Memoize_correctly_if_the_argument_is_null()
		{
			//Arrange
			var fnSpy = new Mock<Func<object, object, object, object, object, object, object, object, object, object, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act, call the function a few times
			memoizedFn.Call(null, null, null, null, null, null, null, null, null, null);
			memoizedFn.Call(null, null, null, null, null, null, null, null, null, null);
			memoizedFn.Call(null, null, null, null, null, null, null, null, null, null);

			//Assert
			fnSpy.Verify(f => f(null, null, null, null, null, null, null, null, null, null), Times.Once());
		}

		[Fact]
		public void Converting_to_Func_implicitly_returns_a_reference_the_Call_method()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int, int, int, int, int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			Func<int, int, int, int, int, int, int, int, int, int, int> asFunc = memoizedFn;

			//Act
			asFunc.Invoke(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			asFunc.Invoke(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			asFunc.Invoke(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

			//Assert
			fnSpy.Verify(f => f(0, 1, 2, 3, 4, 5, 6, 7, 8, 9), Times.Once());
		}

		[Fact]
		public void Calling_reset_will_clear_all_memoized_information()
		{
			//Arrange
			var fnSpy = new Mock<Func<int, int, int, int, int, int, int, int, int, int, int>>();

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			//Act
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

			memoizedFn.Reset();

			memoizedFn.Call(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

			//Assert
			fnSpy.Verify(f => f(0, 1, 2, 3, 4, 5, 6, 7, 8, 9), Times.Exactly(2));
		}

		[Fact]
		public void Uses_supplied_equality_comparer_to_test_arguments_equaility()
		{
			//Arrange
			var fnSpy = new Mock<Func<string, string, string, string, string, string, string, string, string, string, string>>();

			var comparerMock = new Mock<IEqualityComparer<string>>();

			//All the strings are equal
			comparerMock.Setup(s => s.Equals(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(true);

			var memoizedFn = Memoizer.Memoize(fnSpy.Object);

			memoizedFn.WithEqualityComparer(comparerMock.Object);

			//Act
			memoizedFn.Call("ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy", "ayYyy");
			memoizedFn.Call("ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy", "ayYYyy");
			memoizedFn.Call("ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy", "ayYYYy");

			//Assert

			//Function is called only once because the comparer returns always 'true'
			fnSpy.Verify(f => f(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());

			//The comparer was called twice, the first time is excluded because there are no previous arguments
			comparerMock.Verify(c => c.Equals(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(20));
		}
	}
}

