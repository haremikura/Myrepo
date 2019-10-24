namespace MVCFramework.Models.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TextFilesList")]
    public partial class TextFilesList : IEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FileId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FileName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Update { get; set; }

        [Column(TypeName = "ntext")]
        public string Text { get; set; }
    }
}