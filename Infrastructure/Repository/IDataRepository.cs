using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IDataRepository<T> where T : class
    {
    
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectByID(int id);
        
        Task Insert(T entity);
        Task Update(T obj);
        Task Delete(T obj);
       

         List<T> Search(string term);

    }
}
