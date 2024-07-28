using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public int Count()
        {
            return _context.Comment.Count();
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

        public List<CommentViewDTO> GetComments()
        {
            return _context.Comment.Select(c => new CommentViewDTO
            {
                Id = c.Id,
                Content = c.Content,
                FilmId = c.FilmId,
                UserID = c.UserID,
                Time = c.Time,
                Count = _context.Comment.Count()
            }).ToList();
        }

        public List<Comment> getCommentsByFilmId(int filmId)
        {
            return _context.Comment.Where(c => c.FilmId == filmId).ToList();
        }

        public List<CommentViewDTO> Search(QueryObject query)
        {
            var comments = _context.Comment.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                comments = comments.Where(c => c.User.UserName.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return comments.Select(c => new CommentViewDTO
            {
                Id = c.Id,
                Content = c.Content,
                FilmId = c.FilmId,
                UserID = c.UserID,
                Time = c.Time,
                Count = comments.Count()
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }
    }
}
