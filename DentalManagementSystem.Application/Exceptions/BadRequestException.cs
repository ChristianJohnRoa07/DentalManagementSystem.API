using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();

        public BadRequestException(string message) : base(message) {}

        public BadRequestException(string message, List<string> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public static BadRequestException InvalidPassword(string? username = null)
              => new BadRequestException($"Invalid password for user '{(string.IsNullOrWhiteSpace(username) ? "undefined" : username)}'.");

    }
}
