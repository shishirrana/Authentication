using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace reviseAuthentication.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Filter.CustomValidation(50,5)]
        [DisplayName("Full name")]
        public string FullName { get; set; }

        public string Address { get; set; }

    }
    
}
