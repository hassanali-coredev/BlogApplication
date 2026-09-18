using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required Name")]
        [MaxLength(100, ErrorMessage = "Name not exceed 100 characters")]
        public string Name { get; set; }    
        public string? Desciption { get; set; }   
        public ICollection<Post> Posts { get; set; }   
    }
}
