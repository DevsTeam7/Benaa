using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var data = await _dbContext.Set<T>().ToListAsync();
            return data;
        }


        public async Task<T> GetById<Tid>(Tid id)
        {
            var data = await _dbContext.Set<T>().FindAsync(id);
            //if (data == null)
            //    throw new NotFound("No data found");
            return data;
        }

        public async Task CreateRange(List<T> model)
        {
            await _dbContext.Set<T>().AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T model)
        {
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Create(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        
    }
}
