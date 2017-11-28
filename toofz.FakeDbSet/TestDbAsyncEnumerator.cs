using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace toofz
{
    // https://msdn.microsoft.com/library/dn314429.aspx
    internal sealed class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private static readonly Task<bool> TrueTask = Task.FromResult(true);
        private static readonly Task<bool> FalseTask = Task.FromResult(false);

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        private readonly IEnumerator<T> inner;

        public T Current => inner.Current;
        object IDbAsyncEnumerator.Current => Current;

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return inner.MoveNext() ?
                TrueTask :
                FalseTask;
        }

        public void Dispose() => inner.Dispose();
    }
}
