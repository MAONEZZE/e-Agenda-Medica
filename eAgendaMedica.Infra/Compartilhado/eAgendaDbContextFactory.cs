﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eAgendaMedica.Infra.Compartilhado
{
    public class eAgendaDbContextFactory : IDesignTimeDbContextFactory<eAgendaMedicaDbContext>
    {
        public eAgendaMedicaDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string connectionString = config.GetConnectionString("SqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new eAgendaMedicaDbContext(optionsBuilder.Options);
        }
    }
}
