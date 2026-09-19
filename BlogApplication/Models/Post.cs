using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApplication.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required Title")]
        [MaxLength(400,ErrorMessage = "Title not exceed 200 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required Content")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Required Author")]
        [MaxLength(100, ErrorMessage = "Author Name not exceed 100 characters")]
        public string Author { get; set; }
        public string FeatureImagePath { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        [ForeignKey("Category")] 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Commet { get; set; }
    }
}
