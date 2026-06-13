using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Patients.Validator
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator() 
        {
            Include(new IPatientDtoValidator());
        }
    }
}
