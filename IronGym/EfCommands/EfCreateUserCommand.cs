using Application.Commands;
using Application.Commands.UserCommands;
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
    public class EfCreateUserCommand : EfBaseCommand, ICreateUserCommand
    {
        public EfCreateUserCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateUserDto request)
        {
            if (!Context.Roles.Any(u => u.Id == request.RoleId))
            {
                throw new EntityNotfoundException("Role");
            }
            if (Context.Users.Any(u => u.Username.ToLower().Contains(request.Username.ToLower())))
            {
                throw new EntityAlreadyExistsException("User with that username");
            }
            if (Context.Users.Any(u => u.Email.ToLower().Contains(request.Email.ToLower())))
            {
                throw new EntityAlreadyExistsException("User with that e-mail");
            }

            Context.Users.Add(new User
            {
                Username = request.Username,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.Now,
                IsDeleted=false,
                RoleId=request.RoleId,
                Avatar=request.Avatar,
                ModifiedAt=null
            });
            Context.SaveChanges();
        }
    }
}
