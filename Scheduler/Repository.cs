using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace Scheduler
{
    class Repository
    {
        public string ConnStr { get; private set; }
        public Repository()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();
            ConnStr = configBuilder.GetConnectionString("DefaultConnection");
        }
    }
}
