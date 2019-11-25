using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFramework.Models.Entity
{
    [Table("Marker")]
    public partial class Marker : IEntity
    {
        private IEntity x;

        public Marker()
        {
        }

        public Marker(IEntity x)
        {
            Marker marker = (Marker)x;
            MarkerId = marker.MarkerId;
            UserId = marker.UserId;
            Name = marker.Name;
            Color = marker.Color;
            DisplayOrder = marker.DisplayOrder;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkerId { get; set; }

        [Key]
        [Column(Order = 1)]
        // [StringLength(20)]
        public int UserId { get; set; }


        [Column(Order = 2)]
        [StringLength(20)]
        public string Name { get; set; }


        [Column(Order = 3)]
        [StringLength(10)]
        public string Color { get; set; }

        [Column(Order = 4)]
        public int DisplayOrder { get; set; }
    }
}