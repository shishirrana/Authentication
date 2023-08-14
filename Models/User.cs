using reviseAuthentication.Filter;
using System.ComponentModel.DataAnnotations;

namespace reviseAuthentication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MyEmailValidation(ErrorMessage = "Invalid Email Address? Try again!")]
        [Required]
        public string Email { get; set; }

        [MaxLength(10)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
