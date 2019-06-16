using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfEditPostCommand : EfBaseCommand, IEditPostCommand
    {
        public EfEditPostCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreatePostDto request)
        {
            var post = Context.Posts.Find(request.Id);
           
            if(post.Heading.ToLower() != request.Heading.ToLower().Trim())
            {
               if(Context.Posts.Any(p=>p.Heading.ToLower() == request.Heading.ToLower().Trim()))
                    throw new EntityAlreadyExistsException("Post with that heading");
                
            }
            if (!Context.Users.Any(u => u.Id == request.UserId))
                throw new EntityNotfoundException("User");

            post.Heading = request.Heading;
            post.Text = request.Text;
            post.Picture = request.Picture;
            post.UserId = post.UserId;
            post.ModifiedAt = DateTime.Now;
            post.CreatedAt = post.CreatedAt;
            post.IsDeleted = request.IsDeleted;

            Context.SaveChanges();
        }
    }
}
