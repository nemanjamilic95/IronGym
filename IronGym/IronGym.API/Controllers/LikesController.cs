using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using Domain;
using IronGym.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IGetLikesCommand _getLikes;
        private readonly ICreateLikeCommand _createLike;
        private readonly IDeleteLikeCommand _deleteLike;

        public LikesController(IGetLikesCommand getLikes, ICreateLikeCommand createLike, IDeleteLikeCommand deleteLike)
        {
            _getLikes = getLikes;
            _createLike = createLike;
            _deleteLike = deleteLike;
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var likes = _getLikes.Execute(id);
            return StatusCode(200, likes);
        }

        // POST: api/Likes
        [LoggedIn]
        [HttpPost]
        public ActionResult Post([FromBody] InsertLikeDto dto)
        {
            try
            {
                _createLike.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

      
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public ActionResult Delete([FromQuery]InsertLikeDto dto)
        {
            try
            {
                _deleteLike.Execute(dto);
                return NoContent();
            }
            catch(EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured.Please try again later.");
            }
        }
    }
}
