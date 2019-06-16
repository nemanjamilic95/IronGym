using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfDeleteLikeCommand : EfBaseCommand, IDeleteLikeCommand
    {
        public EfDeleteLikeCommand(IronContext context) : base(context)
        {
        }

        public void Execute(InsertLikeDto request)
        {
            var like = Context.Likes.Where(l => l.PostId == request.IdPost && l.UserId == request.IdUser).FirstOrDefault();
            if (like == null)
                throw new EntityNotfoundException("Like");

            Context.Remove(like);
            Context.SaveChanges();
        }
    }
}
