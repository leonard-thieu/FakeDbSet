using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace toofz.Tests
{
    public class FakeDbSetTests
    {
        public FakeDbSetTests()
        {
            var data = new List<object>();
            dbSet = new FakeDbSet<object>(data);
        }

        private readonly FakeDbSet<object> dbSet;

        public class Constructor
        {
            [Fact]
            public void DataIsNull_ThrowsArgumentNullException()
            {
                // Arrange
                IEnumerable<object> data = null;

                // Act -> Assert
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new FakeDbSet<object>(data);
                });
            }

            [Fact]
            public void ReturnsInstance()
            {
                // Arrange
                var data = new List<object>();

                // Act
                var mockDbSet = new FakeDbSet<object>(data);

                // Assert
                Assert.IsAssignableFrom<FakeDbSet<object>>(mockDbSet);
            }
        }

        public class IDbAsyncEnumerable_TEntity_Tests : FakeDbSetTests
        {
            public IDbAsyncEnumerable_TEntity_Tests()
            {
                dbAsyncEnumerable = dbSet;
            }

            private readonly IDbAsyncEnumerable<object> dbAsyncEnumerable;

            public class IDbAsyncEnumerable_TEntity_GetAsyncEnumeratorMethod : IDbAsyncEnumerable_TEntity_Tests
            {
                [Fact]
                public void ReturnsAsyncEnumerator()
                {
                    // Arrange -> Act
                    var asyncEnumerator = dbAsyncEnumerable.GetAsyncEnumerator();

                    // Assert
                    Assert.IsAssignableFrom<IDbAsyncEnumerator<object>>(asyncEnumerator);
                }
            }
        }

        public class IQueryableTests : FakeDbSetTests
        {
            public IQueryableTests()
            {
                queryable = dbSet;
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

            public class ExpressionProperty : IQueryableTests
            {
                [Fact]
                public void ReturnsExpression()
                {
                    // Arrange -> Act
                    var expression = queryable.Expression;

                    // Assert
                    Assert.IsAssignableFrom<Expression>(expression);
                }
            }

            public class ElementTypeProperty : IQueryableTests
            {
                [Fact]
                public void ReturnsElementType()
                {
                    // Arrange -> Act
                    var elementType = queryable.ElementType;

                    // Assert
                    Assert.IsAssignableFrom<Type>(elementType);
                }
            }
        }

        public class IEnumerable_TEntity_Tests : FakeDbSetTests
        {
            public IEnumerable_TEntity_Tests()
            {
                enumerable = dbSet;
            }

            private readonly IEnumerable<object> enumerable;

            public class GetEnumeratorMethod : IEnumerable_TEntity_Tests
            {
                [Fact]
                public void ReturnsEnumerator()
                {
                    // Arrange -> Act
                    var enumerator = enumerable.GetEnumerator();

                    // Assert
                    Assert.IsAssignableFrom<IEnumerator<object>>(enumerator);
                }
            }
        }
    }
}
