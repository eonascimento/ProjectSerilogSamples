using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flogging.Data.CustomDapper
{
    public static class DapperExtensions
    {
        public static int DapperProcNonQuery(this IDbConnection db, string procName, object paramList = null,
            IDbTransaction trans = null, int? timeoutsecond = null)
        {
            
            try
            {
                return db.Execute(procName, paramList, trans, timeoutsecond, CommandType.StoredProcedure);
            }
            catch (Exception orig)
            {
                var ex = new Exception($"Dapper proc execution falled",orig);
                AddDetailsToExcepetion(ex, procName, paramList);
                throw ex;
            }
        }

        private static void AddDetailsToExcepetion(Exception ex, string procName, object paramList)
        {
            ex.Data.Add("Procedure", procName);
            if(paramList is DynamicParameters dynamicParams)
            {
                foreach (var p in dynamicParams.ParameterNames)
                {
                    ex.Data.Add(p, dynamicParams.Get<object>(p).ToString());
                }
            }
            else
            {
                var props = paramList.GetType().GetProperties();
                foreach (var prop in props)
                {
                    ex.Data.Add(prop.Name, prop.GetValue(paramList).ToString());
                }

            }
        }
    }
}
