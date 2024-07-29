using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context){
            _context = context;
        }

        public void Add(T entity){
            _context.Set<T>().Add(entity);
        }

        public void AddAll(ICollection<T> entities){
            _context.Set<T>().AddRange(entities);
        }

        public void Delete(int id)
        {
            var q = GetById(id);
            _context.Set<T>().Remove(q);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().Select(a=>a).ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public Task<int> SaveChanges()
        {
            return  _context.SaveChangesAsync();
        }
    }
}