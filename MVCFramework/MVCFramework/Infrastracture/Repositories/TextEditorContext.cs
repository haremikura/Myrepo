using MVCFramework.Models.Entity;
using System.Data.Entity;

namespace MVCFramework.Infrastracture.Repositries
{
    public class TextEditorContext : DbContext, IDbContext
    {
        public TextEditorContext() : base("TextEditorContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        //public virtual DbSet<CurrentSession> CurrentSession { get; set; }
        public virtual DbSet<Marker> Marker { get; set; }
        public virtual DbSet<MarkingLog> MarkingLog { get; set; }
        public virtual DbSet<ServiceUser> ServiceUser { get; set; }
        public virtual DbSet<TextFilesList> TextFilesList { get; set; }
        public virtual DbSet<EditText> EditText { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marker>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Marker>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<MarkingLog>()
                .Property(e => e.Color)
                .IsFixedLength();
        }

        void IDbContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}