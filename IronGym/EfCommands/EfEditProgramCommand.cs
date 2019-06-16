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
    public class EfEditProgramCommand : EfBaseCommand, IEditProgramCommand
    {
        public EfEditProgramCommand(IronContext context) : base(context)
        {
        }

        public void Execute(CreateProgramDto request)
        {
            var program = Context.Programs.Find(request.Id);

            if (program == null)
                throw new EntityNotfoundException("Program");

            if (program.Heading.ToLower() != request.Heading.ToLower().Trim())
            {
                if (Context.Programs.Any(p => p.Heading.ToLower() == request.Heading.ToLower().Trim()))
                    throw new EntityAlreadyExistsException("Program with that heading");

            }
            program.Heading = request.Heading;
            program.Text = request.Text;
            program.Picture = request.Picture;
            program.IsDeleted = program.IsDeleted;
            program.ModifiedAt = DateTime.Now;
            program.CreatedAt = program.CreatedAt;
            Context.SaveChanges();
        }
    }
}
