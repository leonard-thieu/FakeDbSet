using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace toofz.Tests
{
    public class TestDbAsyncEnumerableTests
    {
        public TestDbAsyncEnumerableTests()
        {
            var expression = Expression.Constant(new[] { 5.5 });
            testDbAsyncEnumerable = new TestDbAsyncEnumerable<double>(expression);
        }

        private readonly TestDbAsyncEnumerable<double> testDbAsyncEnumerable;

        public class Constructor
        {
            [Fact]
            public void ReturnsInstance()
            {
                // Arrange
                var expression = Expression.Constant(new[] { 5.5 });

                // Act
                var enumerable = new TestDbAsyncEnumerable<double>(expression);

                // Assert
                Assert.IsType<TestDbAsyncEnumerable<double>>(enumerable);
            }
        }

        public class IDbAsyncEnumerable_T_Tests : TestDbAsyncEnumerableTests
        {
            public IDbAsyncEnumerable_T_Tests()
            {
                dbAsyncEnumerable = testDbAsyncEnumerable;
            }

            private readonly IDbAsyncEnumerable<double> dbAsyncEnumerable;

            public class GetAsyncEnumeratorMethod : IDbAsyncEnumerable_T_Tests
            {
                [Fact]
                public void ReturnsAsyncEnumerator()
                {
                    // Arrange -> Act
                    var asyncEnumerator = dbAsyncEnumerable.GetAsyncEnumerator();

                    // Assert
                    Assert.IsAssignableFrom<IDbAsyncEnumerator<double>>(asyncEnumerator);
                }
            }
        }

        public class IDbAsyncEnumerableTests : TestDbAsyncEnumerableTests
        {
            public IDbAsyncEnumerableTests()
            {
                dbAsyncEnumerable = testDbAsyncEnumerable;
            }

            private readonly IDbAsyncEnumerable dbAsyncEnumerable;

            public class GetAsyncEnumeratorMethod : IDbAsyncEnumerableTests
            {
                [Fact]
                public void ReturnsAsyncEnumerator()
                {
                    // Arrange -> Act
                    var asyncEnumerator = dbAsyncEnumerable.GetAsyncEnumerator();

                    // Assert
                    Assert.IsAssignableFrom<IDbAsyncEnumerator>(asyncEnumerator);
                }
            }
        }

        public class IQueryableTests : TestDbAsyncEnumerableTests
        {
            public IQueryableTests()
            {
                queryable = testDbAsyncEnumerable;
            }

            private readonly IQueryable queryable;

            public class ProviderProperty : IQueryableTests
            {
                [Fact]
                public void ReturnsProvider()
                {
                    // Arrange -> Act
                    var provider = queryable.Provider;

                    // Assert
                    Assert.IsAssignableFrom<IQueryProvider>(provider);
                }
            }
        }
    }
}
