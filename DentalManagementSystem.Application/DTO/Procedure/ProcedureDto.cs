using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Procedure
{
    public class ProcedureDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public bool IsActive { get; set; }
    }
}
