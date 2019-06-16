using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteRoleCommand : EfBaseCommand, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(IronContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var role = Context.Roles.Find(request);

            if (role == null)
            {
                throw new EntityNotfoundException("Role");
            }

            role.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
