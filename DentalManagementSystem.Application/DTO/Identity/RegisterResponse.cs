using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Identity
{
    public class RegisterResponse
    {
        public bool CreationStatus { get; set; }
        public string UserId { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
