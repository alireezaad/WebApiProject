using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    [Table(name: "Twitts")]
    public class User
    {
        public User() { }
        public User(string username, string pass, string email) {
        
            this.Username = username;
            this.Password = pass;
            this.Email = email;
        }   
        [Key]
        public int Id { get; set; }
        //[Required]
        //[StringLength(1000, ErrorMessage = "تعداد کاراکتر باید بین 5 تا 15 باشد", MinimumLength = 5)]
        //[DataType(DataType.MultilineText)]
        //[Display(Name = "متن")]
        [MaxLength(20)]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }
        //[Required]
        [DataType(DataType.Password)]
        [MaxLength(15)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [MaxLength(30)]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
