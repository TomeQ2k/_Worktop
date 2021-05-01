using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Worktop.Data.Configs;
using Worktop.Models.Domain;

namespace Worktop.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        #region tables

        public DbSet<Job> Jobs { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Models.Domain.File> Files { get; set; }
        public DbSet<Models.Domain.Directory> Directories { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfig().Configure(modelBuilder.Entity<User>());
            new RoleConfig().Configure(modelBuilder.Entity<Role>());
            new UserRoleConfig().Configure(modelBuilder.Entity<UserRole>());

            new TaskConfig().Configure(modelBuilder.Entity<TaskItem>());

            new MessageConfig().Configure(modelBuilder.Entity<Message>());

            new ConnectionConfig().Configure(modelBuilder.Entity<Connection>());

            new FileConfig().Configure(modelBuilder.Entity<Models.Domain.File>());
            new DirectoryConfig().Configure(modelBuilder.Entity<Models.Domain.Directory>());
        }
    }
}