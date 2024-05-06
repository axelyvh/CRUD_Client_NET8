using System.Linq.Expressions;

namespace Application.Repositories.Base
{
    public interface IGenericAsyncRepository<T> where T : class
    {
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
