using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    [Table(name: "Twitts")]
    public class Twitt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "تعداد کاراکتر باید بین 5 تا 15 باشد", MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "متن")]
        public string Body { get; set; }

    }
}
