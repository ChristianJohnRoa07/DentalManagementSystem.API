using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string resource) : base($"{resource} not found.") { }

        public static NotFoundException Username(string? username = null) => new($"User `{(string.IsNullOrWhiteSpace(username) ? "does not exist" : username)}`");
        public static NotFoundException Role(string? role = null) => new($"Role `{(string.IsNullOrWhiteSpace(role) ? "does not exist" : role)}`");

    }
}
