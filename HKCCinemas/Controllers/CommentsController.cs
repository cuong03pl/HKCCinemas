using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.Repo;
using HKCCinemas.Interfaces;
using HKCCinemas.DTO;
using AutoMapper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ICommentRepo commentRepo;
        private readonly IMapper _mapper;

        public CommentsController(CinemasContext context, ICommentRepo commentRepo, IMapper mapper)
        {
            _context = context;
            this.commentRepo = commentRepo;
            _mapper = mapper;
        }
        [HttpGet("GetAllComment")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComment()
        {
            var data = _mapper.Map<List<Comment>>(commentRepo.GetComments());
            return Ok(data);
        }
        [HttpGet("/getcommentbyfilmid/{filmid}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByFilmId(int filmid)
        {
            var data = _mapper.Map<List<Comment>>( commentRepo.getCommentsByFilmId(filmid));
            return Ok(data);
        }
        [HttpGet("checkCommentCurrentUser")]
        public ActionResult<bool> checkCommentCurrentUser(int cmtId, string userId)
        {
            return commentRepo.checkCommentCurrentUser(cmtId, userId);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromForm] CommentDTO comment)
        {
            if (commentRepo.createComment(comment))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest("Them that bai");
        }

        [HttpDelete("deleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!commentRepo.deleteComment(id))
            {
                return BadRequest("Xóa không thành công");
            }
            else return Ok("Xóa thành công");
        }

       
    }
}
