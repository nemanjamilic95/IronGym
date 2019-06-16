using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetRolesCommand : EfBaseCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(IronContext context) : base(context)
        {
        }

        public IEnumerable<GetRolesDto> Execute(RoleSearch request)
        {
            var query = Context.Roles.AsQueryable();

            if (request.Name != null)
            {
                 if (!query.Any(r => r.Name.ToLower().Contains(request.Name.ToLower())))
            {
                throw new EntityNotfoundException("Role");
            }
                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(c => c.IsDeleted == false);
            }

            if (request.Id.HasValue)
            {
                if (!query.Any(r => r.Id==request.Id))
                {
                    throw new EntityNotfoundException("Role");
                }

                query = query.Where(r => r.Id == request.Id);
            }

            return query.Include(r => r.Users).
                Select(r => new GetRolesDto {
                    Id=r.Id,
                    Name=r.Name,
                    Users=r.Users.Where(u=>u.RoleId==r.Id).
                    Select(u=>new RoleUserDto {
                        FirstName=u.FirstName,
                        LastName=u.LastName
                    }).ToList()
                }).ToList();
        }
    }
}
