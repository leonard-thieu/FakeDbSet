using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace toofz
{
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IDbAsyncEnumerable<TEntity>, IQueryable<TEntity>
        where TEntity : class
    {
        public FakeDbSet(IEnumerable<TEntity> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            queryable = data.AsQueryable();
        }

        private readonly IQueryable<TEntity> queryable;

        IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<TEntity>(queryable.Provider);
        Expression IQueryable.Expression => queryable.Expression;
        Type IQueryable.ElementType => queryable.ElementType;
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => queryable.GetEnumerator();
        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator() => new TestDbAsyncEnumerator<TEntity>(queryable.GetEnumerator());
    }
}
