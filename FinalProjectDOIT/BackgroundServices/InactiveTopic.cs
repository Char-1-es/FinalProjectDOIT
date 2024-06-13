using FinalProjectDOIT.Entities;
using System.Linq;
using FinalProjectDOIT.Repos.Interfaces;
using Microsoft.Extensions.Logging;

namespace FinalProjectDOIT.BackgroundServices
{
    public class InactiveTopic : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InactiveTopic> _logger;
        private const int DaysUntilInactive = 7;
        private static readonly TimeSpan CheckInterval = TimeSpan.FromDays(1);

        public InactiveTopic(IServiceProvider serviceProvider,ILogger<InactiveTopic> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Inactive Topic Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndMarkInactiveTopicsAsync();
                await Task.Delay(CheckInterval, stoppingToken);
            }

            _logger.LogInformation("Inactive Topic Service is stopping.");
        }

        private async Task CheckAndMarkInactiveTopicsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var topicRepository = scope.ServiceProvider.GetRequiredService<IRepository<Topic, int>>();

            var topics = await topicRepository.GetAllAsync();

            foreach (var topic in topics)
            {
                var topicWithComments = await topicRepository.GetOneWithCommentsAsync(topic.Id);
                var lastComment = topicWithComments.Comments.OrderByDescending(c => c.CreationDate).FirstOrDefault();

                if (lastComment != null && IsTopicInactive(lastComment.CreationDate))
                {
                    topic.Status = TopicStatus.Inactive;
                    await topicRepository.UpdateAsync(topic);
                    _logger.LogInformation($"Topic {topic.Id} marked as inactive.");
                }
            }
        }
        private static bool IsTopicInactive(DateTime lastCommentDate)
        {
            return (DateTime.UtcNow - lastCommentDate).TotalDays > DaysUntilInactive;
        }
    }
}
