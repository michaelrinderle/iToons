using Microsoft.EntityFrameworkCore;

namespace iToons.Library.Entity
{
    public class iToonsContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MetaData> MetaDatas { get; set; }

        public iToonsContext() { }
    }
}