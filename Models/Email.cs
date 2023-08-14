using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace reviseAuthentication.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

      
        [EmailAddress]
        public string To { get; set; }

        [MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
