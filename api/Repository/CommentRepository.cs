using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentToDelete = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentToDelete == null)
            {
                return null;
            }
            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
            return commentToDelete;

        }

        public async  Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(a => a.appUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            Comment? comment = await _context.Comments.Include(a => a.appUser).FirstOrDefaultAsync(c => c.Id == id);
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingcomment = await _context.Comments.FindAsync(id);
            if (existingcomment == null)
            {
                return null;
            }

            existingcomment.Title = commentModel.Title;
            existingcomment.Context = commentModel.Context;

            await _context.SaveChangesAsync();
            return existingcomment;

        }

        
    }
}