using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Domain.Entities.Common
{
    public class BaseDomainEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
