using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using QuanLyCanHoVaCuDan.Data;

namespace QuanLyCanHoVaCuDan.DAL.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal QuanLyCanHoVaCuDanContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(QuanLyCanHoVaCuDanContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity tEntity)
        {
            await dbSet.AddAsync(tEntity);
        }

        public async Task UpdateAsync(TEntity tEntity)
        {
            dbSet.Attach(tEntity);
            context.Entry(tEntity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TEntity tEntity)
        {
            if (context.Entry(tEntity).State == EntityState.Detached)
            {
                dbSet.Attach(tEntity);
            }
            dbSet.Remove(tEntity);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return dbSet.Find(id) != null;
        }

        
    }
}
