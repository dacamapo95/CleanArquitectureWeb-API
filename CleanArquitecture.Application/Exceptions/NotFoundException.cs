using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArquitecture.Application.Exceptions
{
    public class NotFoundException : ApplicationException 
    {
        public NotFoundException(string message) : base(message) 
        {
        }
        
        public NotFoundException(string name, object key) : base($"Entity {name}, with id: {key} not found.")
        {
        }
    }
}
