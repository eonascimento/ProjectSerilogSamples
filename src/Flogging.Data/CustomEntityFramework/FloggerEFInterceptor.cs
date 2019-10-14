using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flogging.Data.CustomEntityFramework
{
    public class FloggerEFInterceptor : IDbCommandInterceptor
    {

        private Exception WrapperEntityFrameworkException(DbCommand command, Exception ex)
        {
            var newException = new Exception("EntityFramework command failed", ex);
            AddParamsException(command.Parameters, newException);
            return newException;
        }

        private void AddParamsException(DbParameterCollection parameters, Exception newException)
        {
            foreach (DbParameter parameter in parameters)
            {
                newException.Data.Add(parameter.ParameterName, parameter.Value.ToString());
            }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception != null)
                interceptionContext.Exception = WrapperEntityFrameworkException(command, interceptionContext.Exception);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {

        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {

        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (interceptionContext.Exception != null)
                interceptionContext.Exception = WrapperEntityFrameworkException(command, interceptionContext.Exception);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {

        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {

        }
    }
}
