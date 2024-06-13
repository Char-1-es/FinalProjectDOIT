using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using FinalProjectDOIT.Entities;

namespace FinalProjectDOIT.Entities
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [ForeignKey(nameof(Topic))]
        public int TopicId { get; set; }

        public required Topic Topic { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }

        public required User User { get; set; }
    }
}
