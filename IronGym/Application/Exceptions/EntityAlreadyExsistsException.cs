using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityType)
            : base(entityType + " already exists.")
        {

        }
    }
}
