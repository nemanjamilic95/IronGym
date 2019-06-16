using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeletePostCommand : EfBaseCommand, IDeletePostCommand
    {
        public EfDeletePostCommand(IronContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var post = Context.Posts.Find(request);

            if (post == null)
                throw new EntityNotfoundException("Post");
            post.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
