using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Data.Sql
{
    public class BaseDataSql
    {
        public BaseDataSql(Logger log)
        {
            Log = log;
        }

        public Logger Log { get; set; }
    }
}
