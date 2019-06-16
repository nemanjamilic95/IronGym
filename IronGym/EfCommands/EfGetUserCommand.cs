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
    public class EfGetUserComand : EfBaseCommand, IGetUserCommand
    {
        public EfGetUserComand(IronContext context) : base(context)
        {
        }

        public GetUsersDto Execute(int request)
        {
            var user = Context.Users.Include(u=>u.Role).Where(u=>u.Id==request).FirstOrDefault();

            if (user == null)            
                throw new EntityNotfoundException("User");


            return new GetUsersDto
            {
               Id=user.Id,
               Username=user.Username,
               Email=user.Email,
               FirstName=user.FirstName,
               LastName=user.LastName,
               Avatar=user.Avatar,
               RoleName=user.Role.Name
            };
        }
    }
}
