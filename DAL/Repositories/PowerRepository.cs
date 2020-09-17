using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class PowerRepository : BaseRepository<Context.AppContext, Power>, IPowerRepository
    {

        #region properties
        private readonly Context.AppContext _context;
        #endregion


        #region constructor
        public PowerRepository(Context.AppContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region methods

        //public void TestTransaction()
        //{
        //    var dbContextTransaction = _context.Database.BeginTransaction();
        //    dbContextTransaction.
        //}

        #endregion
    }
}
