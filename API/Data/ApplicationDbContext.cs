using API.Data;
using Microsoft.EntityFrameworkCore;

namespace MyProject.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
    {
        
    }
    public DbSet<EMailData> EmailDatas { get; set; }
}
 