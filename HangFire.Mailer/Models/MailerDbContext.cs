using System.Data.Entity;

namespace HangFire.Mailer.Models
{
    public class MailerDbContext : DbContext
    {
        public MailerDbContext()
            : base("MailerDb")
        {
            Database.SetInitializer<MailerDbContext>(null);            
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<ClienteEvento> ClienteEventos { get; set; }
   

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEvento>()
                .ToTable("tbClientesEventos");


            base.OnModelCreating(modelBuilder);
        }
    }
}