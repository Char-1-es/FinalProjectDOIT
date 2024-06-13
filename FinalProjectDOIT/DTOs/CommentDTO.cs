
using System.ComponentModel.DataAnnotations;

public class CommentDTO
{
    [Required(ErrorMessage = "Content is required")]
    [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
    public required string Content { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "TopicId is required")]
    public int TopicId { get; set; }

    [Required(ErrorMessage = "UserEmail is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string UserEmail { get; set; }
}
