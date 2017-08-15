using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;  
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTE_BL.Entities;
using com.Xgensoftware.Core;
using com.Xgensoftware.DAL;

namespace GTE_BL
{
    public class BaseRepository : IDisposable
    {
        #region " Member Variables "
        protected IDataProvider _dbProvider;

        #endregion

        #region " Constructor "
        public BaseRepository()
        {
            this._dbProvider = DALFactory.CreateSqlProvider(DatabaseProvider_Type.MSSQLProvider, ApplicationConfiguration.DBConnection);
        }
        #endregion

        #region " Methods "
        public virtual List<T> FillCollection<T>(CommandType commandType, string spName, List<SQLParam> parms)
        {
            ConcurrentBag<T> collection = new ConcurrentBag<T>();
            DataTable dtCollection = null;

            dtCollection = this._dbProvider.GetData(commandType, spName, parms);

            Parallel.ForEach(dtCollection.AsEnumerable(), d =>
            {
                IEntity e = (IEntity)Activator.CreateInstance<T>();
                e.LoadFrom(d);
                collection.Add((T)e);
            });

            return collection.ToList<T>();

        }
        #endregion

        #region " Virtual Method "
        public virtual void Dispose()
        {
            if (_dbProvider != null)
                _dbProvider = null;
        }
        #endregion
    }
}
