using DentalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Domain.Entities
{
    public class PatientImage : BaseDomainEntity
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;

        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
    }
}
