using DentalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Domain.Entities
{
    public class Procedure : BaseDomainEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public bool IsActive { get; set; }
    }
}
