using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using Memoization;

namespace Memoization.Tests
{
    public class Memoizer_1_Test
    {
        [Fact]
        public void Call_invokes_original_function_the_first_time()
        {
            //Arrange
            var fnSpy = new Mock<Func<int, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            //Act
            memoizedFn.Call(0);

            //Assert
            fnSpy.Verify(f => f(0), Times.Once());
        }

        [Fact]
        public void Call_does_not_invoke_original_function_if_the_arguments_are_Equal()
        {
            //Arrange
            var fnSpy = new Mock<Func<int, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            //Act, call the function a few times
            memoizedFn.Call(0);
            memoizedFn.Call(0);
            memoizedFn.Call(0);

            //Assert
            fnSpy.Verify(f => f(0), Times.Once());
        }

        [Fact]
        public void Call_invokes_original_function_when_arguments_changes()
        {
            //Arrange
            var fnSpy = new Mock<Func<int, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            //Act, call the function a few times changing arguments
            memoizedFn.Call(0);
            memoizedFn.Call(0);
            memoizedFn.Call(10);
            memoizedFn.Call(10);

            //Assert
            fnSpy.Verify(f => f(0), Times.Once());
            fnSpy.Verify(f => f(10), Times.Once());
        }

        [Fact]
        public void Memoize_correctly_if_the_argument_is_null()
        {
            //Arrange
            var fnSpy = new Mock<Func<object, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            //Act, call the function a few times
            memoizedFn.Call(null);
            memoizedFn.Call(null);
            memoizedFn.Call(null);

            //Assert
            fnSpy.Verify(f => f(null), Times.Once());
        }

        [Fact]
        public void Converting_to_Func_implicitly_returns_a_reference_the_Call_method()
        {
            //Arrange
            var fnSpy = new Mock<Func<int, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            Func<int, int> asFunc = memoizedFn;

            //Act
            asFunc.Invoke(0);
            asFunc.Invoke(0);
            asFunc.Invoke(0);

            //Assert
            fnSpy.Verify(f => f(0), Times.Once());
        }

        [Fact]
        public void Calling_reset_will_clear_all_memoized_information()
        {
            //Arrange
            var fnSpy = new Mock<Func<int, int>>();

            var memoizedFn = Memoizer.Memoize(fnSpy.Object);

            //Act
            memoizedFn.Call(0);
            memoizedFn.Call(0);
            memoizedFn.Call(0);

            memoizedFn.Reset();

            memoizedFn.Call(0);

            //Assert
            fnSpy.Verify(f => f(0), Times.Exactly(2));
        }
    }
}
