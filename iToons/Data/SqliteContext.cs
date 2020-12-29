using iToons.Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace iToons.Data
{
    public class SqliteContext : iToonsContext
    {
        public SqliteContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=iToonsDB.db3", 
                b=>b.MigrationsAssembly("iToons"));
    }
}
