using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_End.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]

        [MinLength(5, ErrorMessage = "Name Min Length Is 5")]
        [MaxLength(80, ErrorMessage = "Name Max Length Is 80")]
        [DisplayName("Student Name")]
        [Column("EmployeeName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]

        [MinLength(50, ErrorMessage = "Description Min Length Is 50")]
        [MaxLength(1500, ErrorMessage = "Description Max Length Is 1500")]
        [DisplayName("Description Name")]
        [Column("DescriptionName")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
