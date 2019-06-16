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
    public class EfEditRoleCommand : EfBaseCommand, IEditRoleCommand
    {
        public EfEditRoleCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateRoleDto request)
        {
            var role = Context.Roles.Find(request.Id);

            if (role == null)
            {
                throw new EntityNotfoundException("Role");
            }
            if (role.Name.ToLower() != request.Name.ToLower().Trim())
            {
                if (Context.Roles.Any(p => p.Name.ToLower() == request.Name.ToLower().Trim()))
                    throw new EntityAlreadyExistsException("Role with that Name");

            }

            role.Name = request.Name;
            role.ModifiedAt = DateTime.Now;
            role.IsDeleted = request.IsDeleted;
            role.CreatedAt = role.CreatedAt;
            Context.SaveChanges();
        }
    }
}
