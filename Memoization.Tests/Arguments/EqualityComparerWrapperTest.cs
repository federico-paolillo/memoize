using FluentAssertions;
using Memoization.Arguments;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Memoization.Tests.Arguments
{
	public class EqualityComparerWrapperTest
	{
		[Fact]
		public void Returns_the_same_instance_of_the_Default_comparer()
		{
			//Arrange, Act
			var defaultComparerWrapper = EqualityComparerWrapper<int>.Default;
			var defaultComparerWrapperAgain = EqualityComparerWrapper<int>.Default;

			//Assert
			defaultComparerWrapper.Should().BeSameAs(defaultComparerWrapperAgain);
		}

		[Fact]
		public void Uses_the_wrapped_comparer_to_compare_arguments()
		{
			//Arrange
			var stringComparerMock = new Mock<IEqualityComparer<string>>();

			stringComparerMock.Setup(c => c.Equals("Same", "Same"))
				.Verifiable();

			var comparerWrapper = new EqualityComparerWrapper<string>(stringComparerMock.Object);

			//Act
			comparerWrapper.Compare("Same", "Same");

			//Assert
			stringComparerMock.Verify();
		}
	}
}
