using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfCreateCommentCommand : EfBaseCommand, ICreateCommentCommand
    {
        public EfCreateCommentCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateCommentDto request)
        {
            
            if (!Context.Users.Any(u => u.Id == request.UserId))
                throw new EntityNotfoundException("User");
            if (!Context.Posts.Any(u => u.Id == request.PostId))
                throw new EntityNotfoundException("Post");
            var comment = Context.Comments;
            comment.Add(new Comment
            {
                Text=request.Text,
                IsDeleted=false,
                PostId=request.PostId,
                UserId=request.UserId                
            });
            Context.SaveChanges();

        }
    }
}
