using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteUserCommand : EfBaseCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(IronContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotfoundException("User");
            }

            user.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
