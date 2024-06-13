using FinalProjectDOIT.Entities;

public class TopicDTO
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string UserEmail { get; set; }
    public TopicState State { get; set; }
    public TopicStatus Status { get; set; }
    public DateTime CreationDate { get; set; }

}
