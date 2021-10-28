using Net_AhmedMohammedRabieAbdElwhab.BL.Interfaces;
using Net_AhmedMohammedRabieAbdElwhab.DL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.BL.Repositry
{
    public class GenericUnitOfWork
    {

        private AppDbContext _dbcontext;
        public Type TheType { get; set; }

        public GenericUnitOfWork()
        {
            _dbcontext = new  AppDbContext();
        }
        public IGenericRepositity<TEntityType> GetRepoInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepositry<TEntityType>(_dbcontext);
        }
        public bool SaveChanges()
        {
            try
            {
                if (_dbcontext.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
