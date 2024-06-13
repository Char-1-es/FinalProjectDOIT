using FinalProjectDOIT.Data;
using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectDOIT.Repos
{
    public class CommentRepository : IRepository<Comment, int>
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetOneAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Topic> GetOneWithCommentsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
