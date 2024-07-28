using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICommentRepo
    {
        public List<CommentViewDTO> GetComments();
        public List<Comment> getCommentsByFilmId(int filmId);
        bool createComment(CommentDTO comment);
        bool checkCommentCurrentUser(int cmtId, string userId);
        bool deleteComment(int cmtId);
        int Count();
        List<CommentViewDTO> Search(QueryObject query);
    }
}
