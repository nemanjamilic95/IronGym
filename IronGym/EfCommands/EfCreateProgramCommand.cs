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
    public class EfCreateProgramCommand : EfBaseCommand, ICreateProgramCommand
    {
        public EfCreateProgramCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateProgramDto request)
        {
            if (Context.Programs.Any(p => p.Heading.ToLower().Contains(request.Heading.ToLower())))
                throw new EntityAlreadyExistsException("Program");

            Context.Programs.Add(new Program
            {
                Heading=request.Heading,
                Text=request.Text,
                IsDeleted=false,
                ModifiedAt=null,
                Picture=request.Picture,
            });
            Context.SaveChanges();
        }
    }
}
