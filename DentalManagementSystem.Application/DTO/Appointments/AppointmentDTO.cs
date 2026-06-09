using DentalManagementSystem.Application.DTO.Common;
using DentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Appointments
{
    public class AppointmentDTO : BaseDTO
    {
        public Patient Patient { get; set; }
        public Procedure Procedure { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentStatus { get; set; }
        public float? Amount { get; set; }
        public Guid? AmountReceivedBy { get; set; } // User instead of GUID
        public DateTime? AmountReceivedDateTime { get; set; }
    }
}
