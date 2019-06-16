using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Application.Exceptions
{
    public class EntityNotfoundException : Exception
    {
        public EntityNotfoundException()
        {
        }

        public EntityNotfoundException(string entity) : base($"{entity} doesn't exist.")
        {
        }        
    }
}
