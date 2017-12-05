using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace toofz
{
    /// <summary>
    /// In memory implementation of <see cref="DbSet{TEntity}"/> suitable for testing.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IDbAsyncEnumerable<TEntity>, IQueryable<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes an instance of the <see cref="FakeDbSet{TEntity}"/> class.
        /// </summary>
        /// <param name="entities">
        /// The collection of entities to initialize the set with.
        /// </param>
        public FakeDbSet(params TEntity[] entities) : this((IEnumerable<TEntity>)entities) { }

        /// <summary>
        /// Initializes an instance of the <see cref="FakeDbSet{TEntity}"/> class.
        /// </summary>
        /// <param name="entities">
        /// The collection of entities to initialize the set with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="entities"/> is null.
        /// </exception>
        public FakeDbSet(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            queryable = entities.AsQueryable();
        }

        private readonly IQueryable<TEntity> queryable;
        
        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator() => new TestDbAsyncEnumerator<TEntity>(queryable.GetEnumerator());
        IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<TEntity>(queryable.Provider);
        Expression IQueryable.Expression => queryable.Expression;
        Type IQueryable.ElementType => queryable.ElementType;
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => queryable.GetEnumerator();
    }
}
