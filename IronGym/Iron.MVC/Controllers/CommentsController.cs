using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iron.MVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICreateCommentCommand _create;
        private readonly IEditCommentCommand _edit;
        private readonly IDeleteCommentCommand _delete;

        public CommentsController(ICreateCommentCommand create, IEditCommentCommand edit, IDeleteCommentCommand delete)
        {
            _create = create;
            _edit = edit;
            _delete = delete;
        }
                                  

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] CreateCommentDto dto)
        {
            try
            {
                _create.Execute(dto);
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            catch (EntityNotfoundException e)
            {
                TempData["errors"] = e.Message;
            }
            catch (Exception)
            {
                TempData["errors"] ="An error occured.";
            }
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

       
        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateCommentDto dto)
        {
            try
            {
                if (dto.Text == null || dto.Text == "")
                    throw new Exception("Text cant't be empty."); 
                _edit.Execute(dto);
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            catch
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromForm]int id)
        {
            try
            {
                if (id == 0)
                    throw new EntityNotfoundException("Comment");
                _delete.Execute(id);

                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            catch
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
    }
}