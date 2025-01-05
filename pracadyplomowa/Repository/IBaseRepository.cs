using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository
{
    public interface IBaseRepository<T>
    {
        public void Add(T entity);

        public void AddAll(ICollection<T> entities);

        public void Delete(int id);

        public void Update(T entity);

        public Task<List<T>> GetAll();

        public T? GetById(int id);

        public Task<int> SaveChanges();

        public void DetachEntity(T entity);
        public void ClearTracker();
    }
}