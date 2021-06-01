using AssetInfo.API.Entities;
using AssetInfo.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.DbContexts
{
    public class AssetInfoContext : DbContext
    {

        public AssetInfoContext(DbContextOptions<AssetInfoContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().HasData(CsvFileReader.GetAssetsFromCsvFile());
  
            base.OnModelCreating(modelBuilder);
        }
    }
}
