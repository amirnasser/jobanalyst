using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;


namespace JobAnalyzer.Data
{
    public partial class JobSearchDbContex : DbContext
    {
        public JobSearchDbContex()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /// read appsetting.json and use Queris from ConnectionStrings and use it to build optionsBuilder
            if (!optionsBuilder.IsConfigured)
            {
                // Load configuration from appsettings.json
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Or AppContext.BaseDirectory
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("default");

                optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("11.5.2-mariadb"));
            }
        }
    }
}
