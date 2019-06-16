using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetPostsCommand : EfBaseCommand, IGetPostsCommand
    {
        public EfGetPostsCommand(IronContext context) : base(context)
        {
        }

        public PagedResponse<GetPostDto> Execute(PostSearch request)
        {
            var query = Context.Posts.AsQueryable();

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
                if (!Context.Users.Any(u => u.Username.ToLower().Contains(request.Username.ToLower())))
                {
                    throw new EntityNotfoundException("User with that username");
                }
                if (!query.Any(p => p.User.Username.ToLower().Contains(request.Username.ToLower())))
                {
                    throw new EntityNotfoundException("There is no post for this user");
                }
                query = query.Where(p => p.User.Username.ToLower().Contains(request.Username.ToLower()));
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(c => c.IsDeleted == false);
            }


            var totalCount = query.Count();

            //Include(c => c.User).Include(c => c.Likes).Include(c => c.Comments).
            query = query.Skip((request.PageNumber - 1) * request.PerPage)
               .Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            return new PagedResponse<GetPostDto>{

                CurrentPage=request.PageNumber,
                PagesCount=pagesCount,
                TotalCount=pagesCount,
                Data=query.Select(p => new GetPostDto
            {
                Id = p.Id,
                Heading = p.Heading,
                Text = p.Text,
                Picture = p.Picture,
                Comments = p.Comments.Select(c => c.Text).Count(),
                Likes = p.Likes.Select(l => l.UserId).Count(),
                UserId=p.User.Id,
                User=p.User.Username

            }).ToList()
            };
           
           
        }
    }
}
