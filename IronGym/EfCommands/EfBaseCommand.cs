using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public abstract class EfBaseCommand
    {
        protected IronContext Context;

        protected EfBaseCommand(IronContext context)
        {
            Context = context;
        }
    }
}
