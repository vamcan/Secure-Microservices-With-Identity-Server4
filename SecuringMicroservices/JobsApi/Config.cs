using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi
{
    public class Config:IConfig
    {
        public bool RunDbMigrations { get; set; }
        public bool SeedDatabase { get; set; }
    }
}
