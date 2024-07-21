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

        public async Task Add(T entity){
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var q = GetById(id);
            _context.Set<T>().Remove(q);
            await _context.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().Select(a=>a).ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        // public int Savechange()
        // {
        //     return db.SaveChanges();
        // }
    }
}