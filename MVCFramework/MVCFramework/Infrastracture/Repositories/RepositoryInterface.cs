using MVCFramework.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace MVCFramework.Infrastracture.Repositries
{
    public interface IDbContext
    {
        DbSet<ServiceUser> ServiceUser { get; set; }
    }
}
