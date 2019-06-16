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
    public class EfGetCommentCommand : EfBaseCommand, IGetCommentCommand
    {
        public EfGetCommentCommand(IronContext context) : base(context)
        {
        }

        public GetCommentsDto Execute(int request)
        {
            //var user = Context.Users.Include(u => u.Role).Where(u => u.Id == request).FirstOrDefault();
            var comment = Context.Comments.Include(c => c.User).Where(c=>c.Id==request).FirstOrDefault();

            if (comment == null)
                throw new EntityNotfoundException("Comment");


            return new GetCommentsDto
            {
                Id=comment.Id,
                Text=comment.Text,
                IdPost=comment.PostId,
                IdUser=comment.UserId,
                Username=comment.User.Username,
                Avatar=comment.User.Avatar
            };
        }
    }
}
