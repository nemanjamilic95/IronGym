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
using EfDataAccess;
using IronGym.MVC.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iron.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IGetPostCommand _getOne;
        private readonly IGetPostsUnpaginated _getAll;
        private readonly ICreatePostCommand _createPost;
        private readonly IEditPostCommand _editPost;
        private readonly IDeletePostCommand _deletePost;
        private readonly IGetCommentsCommand _comments;
        private readonly IronContext Context;

        public PostsController(IGetPostCommand getOne, IGetPostsUnpaginated getAll, ICreatePostCommand createPost, IEditPostCommand editPost, IDeletePostCommand deletePost, IGetCommentsCommand comments, IronContext context)
        {
            _getOne = getOne;
            _getAll = getAll;
            _createPost = createPost;
            _editPost = editPost;
            _deletePost = deletePost;
            _comments = comments;
            Context = context;
        }



        // GET: Posts
        public ActionResult Index([FromForm] string Username,[FromForm] string Keyword)
        {
            var search = new PostSearch
            {
                Keyword = Keyword,
                Username = Username
            };
            //Ovaj deo ne mogu da paginujem zbog toga sto mi kod kuce ne radi paginacija. 
            try
            {
                var posts = _getAll.Execute(search);


                return View(posts);
            }
            catch (EntityNotfoundException e)
            {
                TempData["error"] = e.Message;
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return View();
           
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id,CommentSearch src)
        {
            src.IdPost = id;
            src.Id = null;
            src.OnlyActive = true;
            
            try
            {
                try
                {
                    var comments = _comments.Execute(src);
                    ViewBag.Comments = comments.Select(c => new GetCommentsDto
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Username = c.Username,
                        Avatar = c.Avatar,
                        IdPost = c.IdPost,
                        IdUser = c.IdUser
                    });
                }
                catch (EntityNotfoundException e)
                {
                    TempData["error"] = e.Message;
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                }


                var post = _getOne.Execute(id);
                return View(post);
            }
            catch (EntityNotfoundException e)
            {
                 TempData["error"] = e.Message;
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return View();
        }

        // GET: Posts/Create
        public ActionResult Create()
        {

            ViewBag.Users = Context.Users.Select(u=>new GetUsersDto
            {
                Id=u.Id,
                FirstName=u.FirstName,
                LastName=u.LastName
            });
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] PostDto p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                TempData["error"] = "Image extension is not allowed.";
            }
            try
            {

                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images", "post", newFileName);

                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreatePostDto
                {
                    Heading = p.Heading,
                    Text = p.Text,
                    Picture = newFileName,
                    UserId = p.UserId
                };
                _createPost.Execute(dto);

                TempData["success"] = "Your post has been created!";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotfoundException e)
            {
                TempData["error"] = e.Message;
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return View();
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Users = Context.Users.Select(u => new GetUsersDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            });

            return View();
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostDto p)
        {
            
            p.Id = id;
           
            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                TempData["error"] = "Image extension is not allowed.";
            }
            try
            {

                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "post", newFileName);

               

                var dto = new CreatePostDto
                {
                    Id=p.Id,
                    Heading = p.Heading,
                    Text = p.Text,
                    Picture = newFileName,
                    UserId = p.UserId,
                    IsDeleted=p.IsDeleted
                };

                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                _editPost.Execute(dto);

                TempData["success"] = "Your post has been edited!";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotfoundException e)
            {
                TempData["error"] = e.Message;
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            ViewBag.Users = Context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
            return View();
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id,GetPostDto dto)
        {
            dto.Id = id;
            var post=_getOne.Execute(id);
            return View(post);
        }

      
        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _deletePost.Execute(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}