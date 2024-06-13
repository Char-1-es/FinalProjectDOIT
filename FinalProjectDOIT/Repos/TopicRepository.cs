using FinalProjectDOIT.Data;
using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectDOIT.Repos
{
    public class TopicRepository : IRepository<Topic, int>
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> CreateAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetOneAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task UpdateAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Topic> GetOneWithCommentsAsync(int id)
        {
            return await _context.Topics.Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
