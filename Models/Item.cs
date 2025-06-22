using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Web.Models
{
    public class Item
    {

        [Key]

        public  int   Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("The Price")]
        [Range(10,1000,ErrorMessage ="value for {0} must be between {1} and {2}.")]
        public decimal Price { get; set; }


        public DateTime CreatedData { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public string? imagePath {  get; set; }


        [NotMapped]
        public IFormFile clientFile { get; set; }

        public Category? Category { get; set; }
             
    }
}
