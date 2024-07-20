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
    public class FavouritesController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IFavouriteRepo favouriteRepo;
        private readonly IMapper _mapper;

        public FavouritesController(CinemasContext context, IFavouriteRepo favouriteRepo, IMapper mapper)
        {
            _context = context;
            this.favouriteRepo = favouriteRepo;
            _mapper = mapper;
        }
        [HttpGet("getAllFavourites")]
        public async Task<ActionResult<IEnumerable<FavouriteDTO>>> GetAllFavourites()
        {
            var data = _mapper.Map<List<FavouriteDTO>>(favouriteRepo.GetFavourites());
            return Ok(data);
        }
        [HttpGet("getFavouritesByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetFavouritesByUserId(string userId )
        {
            var data = _mapper.Map<List<FilmDTO>>(favouriteRepo.getFavouritesByUserId(userId));
            return Ok(data);
        }
       

        [HttpPost]
        public async Task<ActionResult<Favourite>> CreateFavourite([FromForm] FavouriteDTO comment)
        {
            if (favouriteRepo.createFavourite(comment))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest("Them that bai");
        }

        [HttpDelete("deleteFavourite/{filmId}/{userId}")]
        public async Task<IActionResult> DeleteFavourite(int filmId, string userId)
        {
            if (!favouriteRepo.deleteFavourite(filmId, userId))
            {
                return BadRequest("Xóa không thành công");
            }
            else return Ok("Xóa thành công");
        }

        [HttpGet("isFavourited/{filmId}/{userId}")]
        public async Task<IActionResult> IsFavourited(int filmId, string userId)
        {
            if (favouriteRepo.isFavourited(filmId, userId))
            {
                return Ok(true);
            }
            else return Ok(false);
        }
    }
}
