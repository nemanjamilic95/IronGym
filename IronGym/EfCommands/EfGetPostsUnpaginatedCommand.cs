using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetPostsUnpaginatedCommand : EfBaseCommand, IGetPostsUnpaginated
    {
        public EfGetPostsUnpaginatedCommand(IronContext context) : base(context)
        {
        }

        public IEnumerable<GetPostDto> Execute(PostSearch request)
        {
            var query = Context.Posts.Where(p=>p.IsDeleted==false).AsQueryable();

            if (request.Keyword != null)
            {
                if (!query.Any(p => p.Heading.ToLower().Contains(request.Keyword.ToLower()) || p.Text.ToLower().Contains(request.Keyword.ToLower())))
                {
                    throw new EntityNotfoundException("Post with that heading or text");
                }
                query = query.Where(p => p.Heading.ToLower().Contains(request.Keyword.ToLower()) || p.Text.ToLower().Contains(request.Keyword.ToLower()));
            }
            if (request.Username != null)
            {
                if (!Context.Users.Any(u=>u.Username.ToLower().Contains(request.Username.ToLower())))
                {
                    throw new EntityNotfoundException("User with that username");
                }
                if (!query.Any(p => p.User.Username.ToLower().Contains(request.Username.ToLower())))
                {
                    throw new EntityNotfoundException("Post for this user");
                }
                query = query.Where(p => p.User.Username.ToLower().Contains(request.Username.ToLower()));
            }

            return query.Select(p => new GetPostDto
            {
                Id = p.Id,
                Heading = p.Heading,
                Text = p.Text,
                Picture = p.Picture,
                Comments = p.Comments.Where(c=>c.IsDeleted==false).Select(c=>c.Text).Count(),
                Likes = p.Likes.Select(l => l.UserId).Count(),
                UserId = p.User.Id,
                User = p.User.Username

            });
           

        }
    }
}
