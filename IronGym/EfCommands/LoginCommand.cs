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
    public class LoginCommand : EfBaseCommand, IAuthorizeCommand
    {
        public LoginCommand(IronContext context) : base(context)
        {
        }

        public GetUsersDto Execute(LoginDto request)
        {
            var user = Context.Users.Include(u=>u.Role).Where(u => u.Username == request.Username && u.Password == request.Password).FirstOrDefault();

            if (user == null)
                throw new EntityNotfoundException("User");

            return new GetUsersDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Id =user.Id,
                RoleName =user.Role.Name
            };
        }
    }
}
