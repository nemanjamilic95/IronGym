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
    public class EfEditCommentCommand : EfBaseCommand, IEditCommentCommand
    {
        public EfEditCommentCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateCommentDto request)
        {
            var comment = Context.Comments.Find(request.Id);

            comment.Text = request.Text;
            comment.CreatedAt = comment.CreatedAt;
            comment.IsDeleted = request.IsDeleted;
            comment.ModifiedAt = DateTime.Now;
            comment.PostId = comment.PostId;
            comment.UserId = comment.UserId;
            Context.SaveChanges();
        }
    }
}
