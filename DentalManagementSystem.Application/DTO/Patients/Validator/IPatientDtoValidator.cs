using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Patients.Validator
{
    public class IPatientDtoValidator : AbstractValidator<IPatientDto>
    {
        public IPatientDtoValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(p => p.MobileNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Matches(@"^(09|\+639)\d{9}$")
                .WithMessage("{PropertyName} must be a valid mobile number starting with 09 or +639 followed by 9 digits (e.g., 09123456789 or +639123456789).");
        }
    }
}
