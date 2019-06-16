using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteProgramCommand : EfBaseCommand, IDeleteProgramCommand
    {
        public EfDeleteProgramCommand(IronContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var program = Context.Programs.Find(request);

            if (program == null)
                throw new EntityNotfoundException("Program");

            program.IsDeleted = false;
            Context.SaveChanges();
        }
    }
}
