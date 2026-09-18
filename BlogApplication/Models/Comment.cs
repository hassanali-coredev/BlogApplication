using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApplication.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required UserName")]
        [MaxLength(100, ErrorMessage = "UserName not exceed 100 characters")]
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; }
        [Required]
        public string Content { get; set; }
        [ForeignKey("Category")]
        public int  PostId { get; set; }
        public Post Post { get; set; }  

    }
}
