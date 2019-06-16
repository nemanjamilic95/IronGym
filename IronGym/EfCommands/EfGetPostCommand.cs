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
    public class EfGetPostCommand : EfBaseCommand, IGetPostCommand
    {
        public EfGetPostCommand(IronContext context) : base(context)
        {
        }

        public GetPostDto Execute(int request)
        {
            var post = Context.Posts.Include(p=>p.Comments).Include(p=>p.Likes).Include(p=>p.User).Where(p=>p.Id==request).FirstOrDefault();

            if (post == null)
                throw new EntityNotfoundException("Post");

            return new GetPostDto
            {
                Id=post.Id,
                Heading=post.Heading,
                Text=post.Text,
                Picture=post.Picture,
                UserId=post.UserId,
                Comments=post.Comments.Select(c=>c.Text).Count(),
                Likes=post.Likes.Select(l=>l.UserId).Count(),
                User=post.User.Username
            };
        }
    }
}
