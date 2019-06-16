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
    public class EfEditUserCommand : EfBaseCommand, IEditUserCommand
    {
        public EfEditUserCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateUserDto request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotfoundException("User");
            }

            if (!Context.Roles.Any(u => u.Id==request.RoleId))
            {
                throw new EntityNotfoundException("Role");
            }
            if (user.Username.ToLower() != request.Username.ToLower().Trim())
            {
                if (Context.Users.Any(p => p.Username.ToLower() == request.Username.ToLower().Trim()))
                    throw new EntityAlreadyExistsException("User with that username");

            }
            if (user.Email.ToLower() != request.Email.ToLower().Trim())
            {
                if (Context.Users.Any(p => p.Email.ToLower() == request.Email.ToLower().Trim()))
                    throw new EntityAlreadyExistsException("User with that e-mail");

            }

            user.IsDeleted = request.IsDeleted;
            user.Username = request.Username;
            user.Password = request.Password;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.ModifiedAt = DateTime.Now;
            user.RoleId = request.RoleId;
            user.Avatar = request.Avatar;
            user.CreatedAt = user.CreatedAt;

            Context.SaveChanges();
        }
    }
}
