using AutoMapper;
using FinalProjectDOIT.Repos;
using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;

public class TopicService
{
    private readonly IRepository<FinalProjectDOIT.Entities.Topic, int> _topicRepository;
    private readonly IMapper _mapper;

    public TopicService(IRepository<FinalProjectDOIT.Entities.Topic, int> topicRepository, IMapper mapper)
    {
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public async Task UpdateTopicStateAsync(int id, TopicState state)
    {
        var topic = await _topicRepository.GetOneAsync(id);
        if (topic == null)
        {
            throw new Exception("Topic not available");
        }
        topic.State = state;
        await _topicRepository.UpdateAsync(topic);
    }

    public async Task UpdateTopicStatus(int id, TopicStatus status)
    {
        var topic = await _topicRepository.GetOneAsync(id);
        if (topic == null)
        {
            throw new Exception("Topic not available");
        }
        topic.Status = status;
        await _topicRepository.UpdateAsync(topic);
    }

    public async Task<TopicDTO> CreateTopicAsync(TopicDTO topicDTO, string userEmail, string UserId)
    {
        var topic = _mapper.Map<FinalProjectDOIT.Entities.Topic>(topicDTO);
        topic.UserEmail = userEmail;
        topic.UserId = UserId;
        topic.State = TopicState.Pending;
        topic.Status = TopicStatus.Active;
        var createdTopic = await _topicRepository.CreateAsync(topic);
        return _mapper.Map<TopicDTO>(createdTopic);
    }

    public async Task<IEnumerable<TopicDTO>> GetAllTopicsAsync()
    {
        var topics = await _topicRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task<TopicDetailDTO> GetTopicByIdAsync(int id)
    {
        var topic = await _topicRepository.GetOneWithCommentsAsync(id);
        return _mapper.Map<TopicDetailDTO>(topic);
    }

    public async Task UpdateTopicAsync(int id, TopicDTO topicDTO)
    {
        var topic = await _topicRepository.GetOneAsync(id);
        if (topic == null)
        {
            throw new Exception("Topic not avaialable");
        }
        _mapper.Map(topicDTO, topic);
        await _topicRepository.UpdateAsync(topic);
    }


    public async Task DeleteTopicAsync(int id)
    {
        await _topicRepository.DeleteAsync(id);
    }
}
