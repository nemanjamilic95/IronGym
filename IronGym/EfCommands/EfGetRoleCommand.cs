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
    public class EfGetRoleCommand : EfBaseCommand, IGetRoleCommand
    {
        public EfGetRoleCommand(IronContext context) : base(context)
        {
        }

        public GetRolesDto Execute(int request)
        {
            var role = Context.Roles.Include(r => r.Users).Where(r => r.Id == request).FirstOrDefault();

            if (role == null)
            {
                throw new EntityNotfoundException("Role");
            }

            return new GetRolesDto
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users.Where(u => u.RoleId == role.Id).
                Select(r=>new RoleUserDto {
                    FirstName=r.FirstName,
                    LastName=r.LastName
                }).ToList()
            };

        }
    }
}
