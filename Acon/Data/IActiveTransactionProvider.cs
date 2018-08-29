
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Acon.Data
{
    public interface IActiveTransactionProvider
    {
        IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args);

        /// <summary>
        ///     Gets the active database connection.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args);
    }
}
