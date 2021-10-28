using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.BL.Interfaces
{
    public interface IGenericRepositity<Tentity> where Tentity : class
    {
        IEnumerable<Tentity> Get();
        IEnumerable<Tentity> Get(int PageSize, int CurrentPage, out int Count);
        Tentity Get(int Id);
        IQueryable<Tentity> GetFiltered(Expression<Func<Tentity, bool>> Where);
        void Add(Tentity Entity);
        void Update(Tentity Entity);
        void Delete(int Id);
    }
}
