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
    public class EfCreateRoleCommand : EfBaseCommand, ICreateRoleCommand
    {
        public EfCreateRoleCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateRoleDto request)
        {
            if (Context.Roles.Any(r => r.Name.ToLower().Contains(request.Name.ToLower())))
            {
                throw new EntityAlreadyExistsException("Role with that name");
            }

            Context.Roles.Add(new Role
            {
                Name = request.Name,
                ModifiedAt = null,
                IsDeleted = false,

            });
            Context.SaveChanges();
        }
    }
}
