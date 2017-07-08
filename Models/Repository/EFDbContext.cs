using System.Web;
using System.Data.Entity;

namespace Bobuv.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + HttpContext.Current.Server.MapPath("~/App_Data/Bobuv.mdf")
                    + ";Integrated Security=True")
        {  }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}