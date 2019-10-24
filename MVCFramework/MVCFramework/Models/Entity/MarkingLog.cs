namespace MVCFramework.Models.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MarkingLog")]
    public partial class MarkingLog : IEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string Color { get; set; }
    }
}