namespace MVCFramework.Models.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ServiceUser")]
    public partial class ServiceUser : IEntity
    {

        public ServiceUser() { }
        public ServiceUser(IEntity x)
        {
            ServiceUser serviceUser = (ServiceUser)x;

            UserId = serviceUser.UserId;
            UserName = serviceUser.UserName;
            Password = serviceUser.Password;

        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}