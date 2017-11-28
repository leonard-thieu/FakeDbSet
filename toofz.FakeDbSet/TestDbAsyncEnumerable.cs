using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace toofz
{
    // https://msdn.microsoft.com/library/dn314429.aspx
    internal sealed class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(Expression expression) : base(expression) { }

        IDbAsyncEnumerator<T> IDbAsyncEnumerable<T>.GetAsyncEnumerator() => new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => ((IDbAsyncEnumerable<T>)this).GetAsyncEnumerator();
        IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<T>(this);
    }
}
