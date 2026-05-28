using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Procedure
{
    public class CreateProcedureDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
    }
}
