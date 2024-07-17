using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVault.DataGeneration
{
    public class GetConfig
    {
        public  IConfigurationRoot config { get;  }

        public GetConfig()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }
    }
}
