using FinalProjectDOIT.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDOIT.Entities
{
    public enum TopicState
    {
        Pending,
        Show,
        Hide
    }

    public enum TopicStatus
    {
        Active,
        Inactive
    }

    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public required string UserEmail { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public TopicState State { get; set; }

        [Required]
        public TopicStatus Status { get; set; }

        [Required]
        [ForeignKey(nameof(Entities.User))]
        public required string UserId { get; set; }
        public User User { get; set; }
        public required ICollection<Comment> Comments { get; set; }
    }
}