using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetProgramCommand : EfBaseCommand, IGetProgramCommand
    {
        public EfGetProgramCommand(IronContext context) : base(context)
        {
        }

        public GetProgramDto Execute(int request)
        {
            var program = Context.Programs.Find(request);

            if (program == null)
                throw new EntityNotfoundException("Program");

            return new GetProgramDto
            {
                Id=program.Id,
                Heading=program.Heading,
                Text=program.Text,
                Picture=program.Picture
            };
        }
    }
}
