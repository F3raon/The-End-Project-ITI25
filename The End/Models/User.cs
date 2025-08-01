using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_End.Models
{
    public class User
    {
        public int userId { get; set; }


        [Required(ErrorMessage = "Frist Name Is Required")]

        [MinLength(5, ErrorMessage = "Frist Name Min Length Is 5")]
        [MaxLength(80, ErrorMessage = "Frist Name Max Length Is 80")]
        [DisplayName("Frist Name Name")]
        [Column("Frist Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]

        [MinLength(5, ErrorMessage = "Last Name Min Length Is 5")]
        [MaxLength(80, ErrorMessage = "Last Name Max Length Is 80")]
        [DisplayName("Last  Name")]
        [Column("LAST Name")]
        public string LName { get; set; }




        [EmailAddress(ErrorMessage = "Please Enter A Valid Email")]
        [StringLength(120, ErrorMessage = "Email Must Be Between 4 and 120", MinimumLength = 4)]
        public string Email { get; set; }


        [MaxLength(40, ErrorMessage = "Password Max Length Is 40")]
        [MinLength(8, ErrorMessage = "Password Min Length Is 8")]
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [MaxLength(40, ErrorMessage = "Confirm Password Max Length Is 40")]
        [MinLength(8, ErrorMessage = "Confirm Password Min Length Is 8")]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }

}
