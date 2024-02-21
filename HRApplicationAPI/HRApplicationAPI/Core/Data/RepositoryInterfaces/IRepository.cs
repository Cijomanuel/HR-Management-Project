using HRApplicationAPI.Core.Data.Entities.Common;
using System.Linq.Expressions;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();

        T GetById(int id);

        T GetById(string id);

        void Add(T entity);
        void Update(T entity);
        void Delete(int id);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
