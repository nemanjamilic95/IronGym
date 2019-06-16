using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetCommentsCommand : EfBaseCommand, IGetCommentsCommand
    {
        public EfGetCommentsCommand(IronContext context) : base(context)
        {
        }

        public IEnumerable<GetCommentsDto> Execute(CommentSearch request)
        {
            var query = Context.Comments.Include(c=>c.Post).Include(c=>c.User).AsQueryable();

            if (request.Id.HasValue)
            {
                if (!query.Any(c => c.Id == request.Id))
                {
                    throw new EntityNotfoundException("Comment");
                }
                query = query.Where(c => c.Id == request.Id);
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(c => c.IsDeleted == false);
            }
           

            if (request.IdUser.HasValue)
            {
                if (!Context.Users.Any(p => p.Id == request.IdUser))
                {
                    throw new EntityNotfoundException("Post");
                }

                if (!query.Any(c => c.UserId == request.IdUser))
                {
                    throw new EntityNotfoundException("User");                   
                }
                query = query.Where(c => c.UserId == request.IdUser);
            }


            if (request.IdPost.HasValue)
            {
                if (!Context.Posts.Any(p => p.Id == request.IdPost))
                {
                    throw new EntityNotfoundException("Post");
                }
                if (!query.Any(c => c.PostId == request.IdPost))
                {
                    throw new EntityNotfoundException("Comments for that post");
                }

                query = query.Where(c => c.PostId == request.IdPost);
            }

            if (request.Keyword != null)
            {
                var keyword = request.Keyword.ToLower();
                if (!query.Any(c => c.Text.ToLower().Contains(keyword)))
                    throw new EntityNotfoundException("Comment with that text");
                query = query.Where(c => c.Text.ToLower().Contains(keyword));
            }

            return query.Include(c => c.User).Select(c => new GetCommentsDto
            {
                Id = c.Id,
                Text = c.Text,
                IdPost = c.PostId,
                IdUser=c.UserId,
                Username=c.User.Username,
                Avatar=c.User.Avatar
            }).ToList();

        }
    }
}
