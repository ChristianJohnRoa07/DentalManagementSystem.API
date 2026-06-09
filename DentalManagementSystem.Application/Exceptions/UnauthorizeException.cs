using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Exceptions
{
    public class UnauthorizeException : ApplicationException
    {
        public UnauthorizeException(string message) : base(message)
        {
        }
    }
}
