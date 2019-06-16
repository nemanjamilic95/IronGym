using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using IronGym.API.DataTransfer;
using IronGym.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IGetPostsCommand _getAll;
        private readonly IGetPostCommand _getOne;
        private readonly ICreatePostCommand _createPost;
        private readonly IEditPostCommand _editPost;
        private readonly IDeletePostCommand _deletePost;
        private readonly LoggedUser _user;

        public PostsController(IGetPostsCommand getAll, IGetPostCommand getOne, ICreatePostCommand createPost, IEditPostCommand editPost, IDeletePostCommand deletePost, LoggedUser user)
        {
            _getAll = getAll;
            _getOne = getOne;
            _createPost = createPost;
            _editPost = editPost;
            _deletePost = deletePost;
            _user = user;
        }


        // GET: api/Posts
        [HttpGet]
        public ActionResult Get([FromQuery] PostSearch search)
        {
            try
            {
                var posts = _getAll.Execute(search);
                return StatusCode(200, posts);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Posts/5
        
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var post = _getOne.Execute(id);
                return StatusCode(200, post);
            }
            catch (EntityNotfoundException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Posts
        [LoggedIn]
        [HttpPost]
        public ActionResult Post([FromForm] PostDto p)
        {
            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "post", newFileName);

                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreatePostDto
                {
                    Heading=p.Heading,
                    Text=p.Text,
                    Picture=newFileName,
                    UserId=p.UserId
                };
                _createPost.Execute(dto);
                return StatusCode(201);
            }
            catch(EntityAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500,"An error occured. Please try again later.");  
            }

        }

        // PUT: api/Posts/5
        [LoggedIn]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] PostDto p)
        {
            p.Id = id;
            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "post", newFileName);

                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreatePostDto
                {
                    Id=p.Id,
                    Heading = p.Heading,
                    Text = p.Text,
                    Picture = newFileName,
                    UserId = p.UserId,
                    IsDeleted=p.IsDeleted
                   
                };
                _editPost.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [LoggedIn]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deletePost.Execute(id);
                return StatusCode(204);
            }
            catch (EntityNotfoundException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later.");
            }
        }
    }
}
