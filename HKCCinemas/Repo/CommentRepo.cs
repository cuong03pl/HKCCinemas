using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class CommentRepo: ICommentRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public CommentRepo(CinemasContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }

        public bool checkCommentCurrentUser(int cmtId, string userId)
        {
            var result = _context.Comment.Where(c => c.Id == cmtId && c.UserID == userId).ToList();
            return result.Any();
        }

        public bool createComment(CommentDTO comment)
        {
            var commentMapper = _mapper.Map<Comment>(comment);
            _context.Comment.Add(commentMapper);
            _context.SaveChanges();
            return true;
        }

        public bool deleteComment(int cmtId)
        {
            var cmt = _context.Comment.Where(c => c.Id == cmtId).FirstOrDefault();
            if(cmt != null)
            {
                _context.Comment.Remove(cmt);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public List<Comment> GetComments()
        {
            return _context.Comment.ToList();
        }

        public List<Comment> getCommentsByFilmId(int filmId)
        {
            return _context.Comment.Where(c => c.FilmId == filmId).ToList();
        }

        public List<Comment> Search(string keyword)
        {
            return _context.Comment.Where(c => c.Film.Title.Contains(keyword)).ToList();
        }
    }
}
