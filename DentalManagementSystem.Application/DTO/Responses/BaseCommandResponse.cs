using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Responses
{
    public class BaseCommandResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
