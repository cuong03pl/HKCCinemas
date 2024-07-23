using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICommentRepo
    {
        public List<Comment> GetComments();
        public List<Comment> getCommentsByFilmId(int filmId);
        bool createComment(CommentDTO comment);
        bool checkCommentCurrentUser(int cmtId, string userId);
        bool deleteComment(int cmtId);

        List<Comment> Search(string keyword);
    }
}
