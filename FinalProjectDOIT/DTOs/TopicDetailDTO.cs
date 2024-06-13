
public class TopicDetailDTO : TopicDTO
{
    public required ICollection<CommentDTO> Comments { get; set; }
}
