using MVCFramework.Models.Entity;
using System.Data.Entity;

namespace MVCFramework.Infrastracture.Repositries
{
    public interface IDbContext
    {
        DbSet<CurrentSession> CurrentSession { get; set; }
        DbSet<Marker> Marker { get; set; }
        DbSet<MarkingLog> MarkingLog { get; set; }
        DbSet<ServiceUser> ServiceUser { get; set; }
        DbSet<TextFilesList> TextFilesList { get; set; }
    }
}