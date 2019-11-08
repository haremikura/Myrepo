using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFramework.Models.Entity
{
    [Table("EditText")]
    public class EditText : IEntity
    {
        private EditText x;

        public EditText()
        {
        }

        public EditText(EditText x)
        {
            FileId = x.FileId;
            Text = x.Text;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FileId { get; set; }

        [Column(TypeName = "ntext")]
        public string Text { get; set; }
    }
}