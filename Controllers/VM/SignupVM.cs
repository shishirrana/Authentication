using reviseAuthentication.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace reviseAuthentication.Controllers.VM
{
    public class SignupVM:User
    {
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
