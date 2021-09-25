using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi
{
    public interface IConfig
    {
        bool RunDbMigrations { get; set; }
        bool SeedDatabase { get; set; }
    }
}
