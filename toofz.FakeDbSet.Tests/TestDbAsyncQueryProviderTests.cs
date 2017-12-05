using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace toofz.Tests
{
    public class TestDbAsyncQueryProviderTests
    {
        public TestDbAsyncQueryProviderTests()
        {
            provider = new TestDbAsyncQueryProvider<double>(mockInner.Object);
        }

        private readonly Mock<IQueryProvider> mockInner = new Mock<IQueryProvider>();
        private readonly TestDbAsyncQueryProvider<double> provider;

        public class Constructor
        {
            [Fact]
            public void ReturnsInstance()
            {
                // Arrange
                var inner = Mock.Of<IQueryProvider>();

                // Act
                var provider = new TestDbAsyncQueryProvider<double>(inner);

                // Assert
                Assert.IsType<TestDbAsyncQueryProvider<double>>(provider);
            }
        }

        public class CreateQueryMethod : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public void ReturnsQuery()
            {
                // Arrange
                var expression = Expression.Constant(5.5);

                // Act
                var query = provider.CreateQuery(expression);

                // Assert
                Assert.IsAssignableFrom<IQueryable>(query);
            }
        }

        public class CreateQuery_TElement_Method : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public void ReturnsQuery()
            {
                // Arrange
                var expression = Expression.Constant(5.5);

                // Act
                var query = provider.CreateQuery<double>(expression);

                // Assert
                Assert.IsAssignableFrom<IQueryable<double>>(query);
            }
        }

        public class ExecuteMethod : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public void ReturnsResult()
            {
                // Arrange
                mockInner.Setup(i => i.Execute(It.IsAny<Expression>())).Returns(5.5);
                var expression = Expression.Constant(5.5);

                // Act
                var result = provider.Execute(expression);

                // Assert
                Assert.Equal(5.5, result);
            }
        }

        public class Execute_TResult_Method : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public void ReturnsResult()
            {
                // Arrange
                mockInner.Setup(i => i.Execute<double>(It.IsAny<Expression>())).Returns(5.5);
                var expression = Expression.Constant(5.5);

                // Act
                var result = provider.Execute<double>(expression);

                // Assert
                Assert.Equal(5.5, result);
            }
        }

        public class ExecuteAsyncMethod : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public async Task ReturnsResult()
            {
                // Arrange
                mockInner.Setup(i => i.Execute(It.IsAny<Expression>())).Returns(5.5);
                var expression = Expression.Constant(5.5);
                var cancellationToken = CancellationToken.None;

                // Act
                var result = await provider.ExecuteAsync(expression, cancellationToken);

                // Assert
                Assert.Equal(5.5, result);
            }
        }

        public class ExecuteAsync_TResult_Method : TestDbAsyncQueryProviderTests
        {
            [Fact]
            public async Task ReturnsResult()
            {
                // Arrange
                mockInner.Setup(i => i.Execute<double>(It.IsAny<Expression>())).Returns(5.5);
                var expression = Expression.Constant(5.5);
                var cancellationToken = CancellationToken.None;

                // Act
                var result = await provider.ExecuteAsync<double>(expression, cancellationToken);

                // Assert
                Assert.Equal(5.5, result);
            }
        }
    }
}
