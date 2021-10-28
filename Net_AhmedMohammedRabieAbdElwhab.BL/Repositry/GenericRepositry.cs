using Net_AhmedMohammedRabieAbdElwhab.BL.Interfaces;
using Net_AhmedMohammedRabieAbdElwhab.DL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.BL.Repositry
{
    public class GenericRepositry<Tentity> : IGenericRepositity<Tentity> where Tentity : class
    {
        DbSet<Tentity> _dbset;
        private AppDbContext _dbContext;
        public GenericRepositry(AppDbContext context)
        {
            _dbContext = context;
            _dbset = _dbContext.Set<Tentity>();

        }
        public void Add(Tentity Entity)
        {
            _dbset.Add(Entity);
        }

        public void Delete(int Id)
        {
            var entity = _dbset.Find(Id);
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbset.Attach(entity);
            _dbset.Remove(entity);
        }

        public IEnumerable<Tentity> Get()
        {
            IEnumerable<Tentity> query = _dbset;
            return query.ToList();
        }

        public IEnumerable<Tentity> Get(int PageSize, int CurrentPage, out int Count)
        {
            IEnumerable<Tentity> query = _dbset;
            Count = query.Count();
            return query.Skip((PageSize * CurrentPage) - PageSize).Take(PageSize).ToList();

        }

        public Tentity Get(int Id)
        {
            return _dbset.Find(Id);
        }

        public IQueryable<Tentity> GetFiltered(Expression<Func<Tentity, bool>> Where)
        {
            return _dbset.Where(Where);
        }

        public void Update(Tentity Entity)
        {
            _dbset.Attach(Entity);
            _dbContext.Entry(Entity).State = EntityState.Modified;
        }
    }
}
