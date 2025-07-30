using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using api.Extensions;
namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _repo;
        private readonly IStockRepository _stockrepo;
        private readonly UserManager<AppUser> _managerRepo;
        public CommentController(ICommentRepository repo, IStockRepository stockrepo, UserManager<AppUser> managerRepo)
        {
            _repo = repo;
            _stockrepo = stockrepo;
            _managerRepo = managerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comments = await _repo.GetAllAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _repo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockid:int}")]
        
        public async Task<IActionResult> Create([FromRoute] int stockid, CreateDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockrepo.StockExists(stockid))
            {
                return BadRequest("Stock does not exists");
            }
            var username = User.GetUsername();
            var appUser = await _managerRepo.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreate(stockid);
            commentModel.AppUserId = appUser.Id;
            
            await _repo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());


        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestDto modeldto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _repo.UpdateAsync(id, modeldto.ToCommentFromUpdate());

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());


        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comToDelete = await _repo.DeleteAsync(id);
            if (comToDelete == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}