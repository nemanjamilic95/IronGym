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
    public class EfCreateLikeCommand : EfBaseCommand, ICreateLikeCommand
    {
        public EfCreateLikeCommand(IronContext context) : base(context)
        {
        }

        public void Execute(InsertLikeDto request)
        {

            if (!Context.Users.Any(u => u.Id == request.IdUser))
            {
                throw new EntityNotfoundException("User");
            }
            if (!Context.Posts.Any(u => u.Id == request.IdPost))
            {
                throw new EntityNotfoundException("Post");
            }

            Context.Likes.Add(new Like
            {
                PostId=request.IdPost,
                UserId=request.IdUser
            });
            Context.SaveChanges();
        }
    }
}
