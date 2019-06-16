using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteCommentCommand : EfBaseCommand, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(IronContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var comment = Context.Comments.Find(request);

            if (comment == null)
                throw new EntityNotfoundException("Comment");

            comment.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
