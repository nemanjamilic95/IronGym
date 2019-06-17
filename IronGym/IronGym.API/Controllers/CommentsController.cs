using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using IronGym.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGetCommentsCommand _getAll;
        private readonly IGetCommentCommand _getOne;
        private readonly ICreateCommentCommand _createComment;
        private readonly IEditCommentCommand _editComment;
        private readonly IDeleteCommentCommand _deleteComment;

        public CommentsController(IGetCommentsCommand getAll, IGetCommentCommand getOne, ICreateCommentCommand createComment, IEditCommentCommand editComment, IDeleteCommentCommand deleteComment)
        {
            _getAll = getAll;
            _getOne = getOne;
            _createComment = createComment;
            _editComment = editComment;
            _deleteComment = deleteComment;
        }

        /// <summary>
        /// Returns all orders that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get from query /Todo
        ///     {
        ///        "id": 1,
        ///        "keyword": "nesto",
        ///        "OnlyActive":true,
        ///        "perPage":1,
        ///        "PageNumber":1,
        ///        "IdPost":1,
        ///        "IdUser":1
        /// 
        ///     }
        ///
        /// </remarks>
        //[LoggedIn]
        [HttpGet]
        public ActionResult Get([FromQuery] CommentSearch search)
        {
            try
            {
                var comments = _getAll.Execute(search);
                return StatusCode(200, comments);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"An error occured. Please try again later");
            }

        }
       
        // GET: api/Comments/5
        //[LoggedIn]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var comment = _getOne.Execute(id);
                return StatusCode(200, comment);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later");
            }
        }
        /// <summary>
        /// Creates a new comment in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "text": "CommentText",
        ///        "userId":1,
        ///        "PostId":1
        ///     }
        ///
        /// </remarks>
        // POST: api/Comments
        // [LoggedIn]
        [HttpPost]
        public ActionResult Post([FromBody] CreateCommentDto dto)
        {
            try
            {
                _createComment.Execute(dto);
                return StatusCode(201, "You inserted comment successfully");
            }
            catch(EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "An error occured. Please try again later.");
            }
        }
        /// <summary>
        /// Updates a comment 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Todo
        ///     {             
        ///        "text": "CommentText"
        ///     }
        ///
        /// </remarks>
        // PUT: api/Comments/5
        //  [LoggedIn]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CreateCommentDto dto)
        {
            dto.Id = id;

            try
            {
                _editComment.Execute(dto);
                return StatusCode(200, "You edited comment successfully.");
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later.");
            }
        }

        // DELETE: api/ApiWithActions/5
       // [LoggedIn]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteComment.Execute(id);
                return StatusCode(204);
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later."); 
            }
        }
    }
}
