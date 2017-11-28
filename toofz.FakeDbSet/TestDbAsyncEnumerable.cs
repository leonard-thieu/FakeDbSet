using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace toofz
{
    // https://msdn.microsoft.com/library/dn314429.aspx
    internal sealed class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(Expression expression) : base(expression) { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator() => new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => GetAsyncEnumerator();
        IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<T>(this);
    }
}
