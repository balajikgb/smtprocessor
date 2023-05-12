using Microsoft.EntityFrameworkCore;
using PriceUpdateRepository.DataModels;

namespace PriceUpdateRepository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProcessClassModel> Processes { get; set; }
        public DbSet<ProcessGroupModel> ProcessesGroup { get; set; }
        public DbSet<ProcessGroupItemsModel> ProcessesGroupItems { get; set; }
        public DbSet<RunControlPageModel> RunControlPage { get; set; }
        public DbSet<ServersModel> Servers { get; set; }
        public DbSet<ScheduleModel> Schedulers { get; set; }
        public DbSet<ProcessRequestModel> ProcessRequest { get; set; }
        public DbSet<ProcessRequestsModel> ProcessRequests { get; set; }
        public DbSet<ProcessRequestvwModel> getprocessrequestdata { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("Users").Property(d => d.CreateDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserModel>().Property(a => a.Active).HasDefaultValue(false);
            modelBuilder.Entity<ProcessClassModel>().ToTable("process");
            modelBuilder.Entity<ProcessGroupModel>().ToTable("processgroup");
            modelBuilder.Entity<ProcessGroupItemsModel>().ToTable("processgroupitems");
            modelBuilder.Entity<RunControlPageModel>().ToTable("runcontrolpage");
            modelBuilder.Entity<ServersModel>().ToTable("servers");
            modelBuilder.Entity<ScheduleModel>().ToTable("schedule");
            modelBuilder.Entity<ProcessRequestModel>().ToTable("processrequest");
            modelBuilder.Entity<ProcessRequestsModel>().ToTable("processrequests");
        }
    }
}
