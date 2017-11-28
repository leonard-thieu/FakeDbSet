using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace toofz
{
    // https://msdn.microsoft.com/library/dn314429.aspx
    internal sealed class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        public TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            this.inner = inner;
        }

        private readonly IQueryProvider inner;

        public IQueryable CreateQuery(Expression expression) => new TestDbAsyncEnumerable<TEntity>(expression);
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestDbAsyncEnumerable<TElement>(expression);
        public object Execute(Expression expression) => inner.Execute(expression);
        public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);
        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute(expression));
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute<TResult>(expression));
    }
}
