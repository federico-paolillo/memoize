using FluentAssertions;
using Memoization.Arguments;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Memoization.Tests
{
	public class ArgumentsComparerTest
	{
		[Fact]
		public void Selects_the_correct_equality_comparer()
		{
			//Arrange
			var stringComparerMock = new Mock<IEqualityComparer<string>>();

			var comparerStore = new ComparersStore();

			comparerStore.Add(stringComparerMock.Object);

			//Act
			var comparer = comparerStore.Get<string>();

			//Assert
			comparer.Should().BeSameAs(stringComparerMock.Object);
		}

		[Fact]
		public void Selects_the_default_equality_comparer_for_types_without_a_comparer()
		{
			//Arrange
			var comparerStore = new ComparersStore();

			var defaultComparer = EqualityComparer<string>.Default;

			//Act
			var comparer = comparerStore.Get<string>();

			//Assert
			comparer.Should().BeSameAs(defaultComparer);
		}
	}
}
