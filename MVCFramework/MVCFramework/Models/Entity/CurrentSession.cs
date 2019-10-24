using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFramework.Models.Entity
{
    [Table("CurrentSession")]
    public partial class CurrentSession : IEntity
    {
        [Key]
        [Column(Order = 0, TypeName = "ntext")]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedAt { get; set; }
    }
}