using System.ComponentModel.DataAnnotations;

namespace Starter.Models
{
    public class UserModel 
    {
        [Key]
        public int id{get;set;}
        [Required]
        [Display(Name = "First Name")]
        public string fName{get;set;}
        [Required]
        [Display(Name = "Last Name")]
        public string lName{get;set;}
        [Range(18,118)]
        [Display(Name = "Age")]
        public int age{get;set;}
        [Required]
        [StringLength(50)]
        [Display(Name = "UserName")]
        public string userName{get;set;}
    }
}