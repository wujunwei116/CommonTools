using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Data
{
    public class ActiveTransactionProviderArgs : Dictionary<string, object>
    {
        public static ActiveTransactionProviderArgs Empty { get { return new ActiveTransactionProviderArgs(); } }
    }
}
