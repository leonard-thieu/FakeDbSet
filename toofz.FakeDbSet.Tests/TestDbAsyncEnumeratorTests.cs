using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace toofz.Tests
{
    public class TestDbAsyncEnumeratorTests
    {
        public TestDbAsyncEnumeratorTests()
        {
            var data = new List<double> { 5.5 };
            var inner = data.GetEnumerator();
            testDbAsyncEnumerator = new TestDbAsyncEnumerator<double>(inner);
        }

        private readonly TestDbAsyncEnumerator<double> testDbAsyncEnumerator;

        public class Constructor
        {
            [Fact]
            public void ReturnsInstance()
            {
                // Arrange
                var inner = new List<double>().GetEnumerator();

                // Act
                var enumerator = new TestDbAsyncEnumerator<double>(inner);

                // Assert
                Assert.IsType<TestDbAsyncEnumerator<double>>(enumerator);
            }
        }

        public class CurrentProperty : TestDbAsyncEnumeratorTests
        {
            [Fact]
            public async Task ReturnsCurrentElement()
            {
                // Arrange
                await testDbAsyncEnumerator.MoveNextAsync(default);

                // Act
                var current = testDbAsyncEnumerator.Current;

                // Assert
                Assert.Equal(5.5, current);
            }
        }

        public class IDbAsyncEnumeratorTests : TestDbAsyncEnumeratorTests
        {
            public IDbAsyncEnumeratorTests()
            {
                dbAsyncEnumerator = testDbAsyncEnumerator;
            }

            private readonly IDbAsyncEnumerator dbAsyncEnumerator;

            public new class CurrentProperty : IDbAsyncEnumeratorTests
            {
                [Fact]
                public async Task ReturnsCurrentElement()
                {
                    // Arrange
                    await testDbAsyncEnumerator.MoveNextAsync(default);

                    // Act
                    var current = dbAsyncEnumerator.Current;

                    // Assert
                    Assert.Equal(5.5, current);
                }
            }
        }

        public class MoveNextAsyncMethod : TestDbAsyncEnumeratorTests
        {
            [Fact]
            public async Task MovesToNextItem()
            {
                // Arrange -> Act
                await testDbAsyncEnumerator.MoveNextAsync(default);

                // Assert
                Assert.Equal(5.5, testDbAsyncEnumerator.Current);
            }

            [Fact]
            public async Task Successful_ReturnsTrue()
            {
                // Arrange -> Act
                var result = await testDbAsyncEnumerator.MoveNextAsync(default);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public async Task NotSuccessful_ReturnsFalse()
            {
                // Arrange
                await testDbAsyncEnumerator.MoveNextAsync(default);

                // Act
                var result = await testDbAsyncEnumerator.MoveNextAsync(default);

                // Assert
                Assert.False(result);
            }
        }

        public class DisposeMethod
        {
            [Fact]
            public void DisposesInner()
            {
                // Arrange
                var mockInner = new Mock<IEnumerator<double>>();
                var enumerator = new TestDbAsyncEnumerator<double>(mockInner.Object);

                // Act
                enumerator.Dispose();

                // Assert
                mockInner.Verify(i => i.Dispose(), Times.Once);
            }
        }
    }
}
