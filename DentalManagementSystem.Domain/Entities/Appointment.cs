using DentalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Domain.Entities
{
    public class Appointment : BaseDomainEntity
    {
        public Patient Patient { get; set; }
        public Procedure Procedure { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentStatus { get; set; }
        public float Amount { get; set; }
        public Guid AmountReceivedBy { get; set; } // User instead of GUID
        public DateTime AmountReceivedDateTime { get; set; }
    }
}
