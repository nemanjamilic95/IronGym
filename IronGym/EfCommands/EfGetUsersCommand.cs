using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetUsersCommand : EfBaseCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(IronContext context) : base(context)
        {
        }

        public PagedResponse<GetUsersDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if (request.Username != null)
            {
                if (query.Any(u => u.Username.ToLower().Contains(request.Username.ToLower())))
                    throw new EntityNotfoundException("User");
                query = query.Where(u => u.Username.ToLower() == request.Username.ToLower());
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(c => c.IsDeleted == false);
            }
            if (request.FirstName != null)
            {
                if (query.Any(u => u.FirstName.ToLower().Contains(request.FirstName.ToLower())))
                    throw new EntityNotfoundException("User");
                query = query.Where(u => u.FirstName.ToLower() == request.FirstName.ToLower());
            }

            if (request.LastName != null)
            {
                if (query.Any(u => u.LastName.ToLower().Contains(request.LastName.ToLower())))
                    throw new EntityNotfoundException("User");
                query = query.Where(u => u.LastName.ToLower() == request.LastName.ToLower());
            }
            if (request.RoleId.HasValue)
            {
                if (!Context.Roles.Any(r=>r.Id==request.RoleId))
                    throw new EntityNotfoundException("Role");
                query = query.Where(u => u.RoleId == request.RoleId);
            }


            var totalCount = query.Count();

            query = query.Include(u=>u.Role).Skip((request.PageNumber - 1) * request.PerPage).Take( request.PerPage);
           
            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            
            var response = new PagedResponse<GetUsersDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(u => new GetUsersDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = u.Avatar,
                    RoleName = u.Role.Name
                })
            };

            return response;
        }
    }
}
