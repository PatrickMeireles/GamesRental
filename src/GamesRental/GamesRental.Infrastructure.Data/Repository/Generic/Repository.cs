using GamesRental.Domain.Interfaces.Generic;
using GamesRental.Entities.Base;
using GamesRental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRental.Infrastructure.Data.Repository.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BaseContext _db;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(BaseContext context)
        {
            _db = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity data)
        {
            await _dbSet.AddAsync(data);
            await _db.SaveChangesAsync();

            return data;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var obj = await _dbSet.FindAsync(id);
                _dbSet.Remove(obj);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TEntity>> GetAll() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetById(int id) => await _dbSet.FindAsync(id);

        public async Task<int> SaveChanges() => await _db.SaveChangesAsync();

        public async Task<TEntity> Update(TEntity data)
        {
            var local = _dbSet.Local.FirstOrDefault(x => x.Id == data.Id);

            if(local != null)
                _db.Entry<TEntity>(local).State = EntityState.Detached;

            _dbSet.Update(data);

            await _db.SaveChangesAsync();
            return data;
        }
    }
}
