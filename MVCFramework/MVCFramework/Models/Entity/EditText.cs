using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFramework.Models.Entity
{
    [Table("EditText")]
    public class EditText : IEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FileId { get; set; }

        [Column(TypeName = "ntext")]
        public string Text { get; set; }
    }
}