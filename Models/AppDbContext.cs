
using Microsoft.EntityFrameworkCore;

namespace AppMvc.models
{
    //AppMvc.models.AppDbContext
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // foreach(var entytiTye in modelBuilder.Model.GetEntityTypes()){
            //     var tableName = entytiTye.GetTableName();
            //     if(tableName.StartsWith("AspNet")){
            //         entytiTye.SetTableName(tableName.Substring(6));
            //     }
            // }
        }
    }
}