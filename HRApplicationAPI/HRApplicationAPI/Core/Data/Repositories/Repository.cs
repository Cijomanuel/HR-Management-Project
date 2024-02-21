using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly HrDbContext context;
        private readonly DbSet<T> entities;

        public Repository(HrDbContext context)
        {
            try
            {
                this.context = context;
                entities = context.Set<T>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Add(T entity)
        {
            try
            {
                entities.Add(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Delete(int id)
        {
            try
            {
                var data = entities.Find(id);
                entities.Remove(data);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {           
            try
            {
                return entities.Where(predicate).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return entities.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public T GetById(int id)
        {
            try
            {
                return entities.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(string id)
        {
            try
            {
                return entities.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
            entities.Update(entity);
            context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
