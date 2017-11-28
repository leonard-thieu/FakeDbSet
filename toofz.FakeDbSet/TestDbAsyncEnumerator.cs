using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace toofz
{
    // https://msdn.microsoft.com/library/dn314429.aspx
    internal sealed class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        private readonly IEnumerator<T> inner;

        public T Current => inner.Current;
        object IDbAsyncEnumerator.Current => Current;
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) => Task.FromResult(inner.MoveNext());
        public void Dispose() => inner.Dispose();
    }
}
