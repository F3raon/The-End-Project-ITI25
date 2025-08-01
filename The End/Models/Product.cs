using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace The_End.Models
{
    public class Product
    {
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Title Is Required")]

        [MinLength(5, ErrorMessage = "Title Min Length Is 5")]
        [MaxLength(80, ErrorMessage = "Title Max Length Is 80")]
        [DisplayName("Product Name")]
        [Column("ProductName")]
        public string Title { get; set; }


        [Range(500, 10000000, ErrorMessage = "Price Must Be Between 500 and 10000000")]
        public decimal Price { get; set; }



        [Required(ErrorMessage = "Description Is Required")]

        [MinLength(50, ErrorMessage = "Description Min Length Is 50")]
        [MaxLength(1500, ErrorMessage = "Description Max Length Is 1500")]
        [DisplayName("Description Name")]
        [Column("DescriptionName")]
        public string Description { get; set; }



        [Range(1, 2000, ErrorMessage = "Quantity Must Be Between 1 and 2000")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Image Is Required")]

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}


