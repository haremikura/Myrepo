using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFramework.Models.Entity
{
    [Table("TextFilesList")]
    public class TextFilesList : IEntity
    {
        public TextFilesList()
        {
        }

        public TextFilesList(IEntity entity)
        {
            TextFilesList setEntity = (TextFilesList)entity;
            FileId = setEntity.FileId;
            UserId = setEntity.UserId;
            Update = setEntity.Update;
        }

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
    }
}