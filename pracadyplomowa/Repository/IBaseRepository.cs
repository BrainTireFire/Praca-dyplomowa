using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository
{
    public interface IBaseRepository<T>
    {
        public Task Add(T entity);

        public void Delete(int id);

        public void Update(T entity);

        public List<T> GetAll();

        public T? GetById(int id);
    }
}