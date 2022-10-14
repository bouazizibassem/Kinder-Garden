using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        List<PortfolioItem> portfolioItems;
        protected readonly DataContext _context;
        protected readonly DbSet<T> _table;
        
        List<Msg> Messages;
        public DataRepository(DataContext context)
        {
            this._context = context;
            this._table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> SelectAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> SelectByID(int id)
        {
            return await _table.FindAsync(id);
        }

       

        public async Task Insert(T entity)
        {
            await _table.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task Update(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T obj)
        {
            _table.Remove(obj);
            await _context.SaveChangesAsync();
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

      
        public void Dispose()
        {
            Dispose(true);

        }




        #endregion

        public List<PortfolioItem> Search(string term)
        {
            var result = _context.portfolioItems.Include(a => a.ResponsableID)
                .Where(b => b.NomJardin.Contains(term)
                        || b.AddressJ.Contains(term)).ToList();

            return result;
        }




        public IList<PortfolioItem> List()
        {
            return portfolioItems;
        }
        public   PortfolioItem Find(int id)
        {
            var portfolioitem = portfolioItems.SingleOrDefault(b => b.Id == id);

            return portfolioitem;
        }

        List<T> IDataRepository<T>.Search(string term)
        {
            throw new NotImplementedException();
        }
    }
}
