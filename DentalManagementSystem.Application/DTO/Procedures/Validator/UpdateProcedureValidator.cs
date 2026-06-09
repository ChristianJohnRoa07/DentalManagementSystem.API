using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Procedures.Validator
{
    public class UpdateProcedureValidator : AbstractValidator<UpdateProcedureDto>
    {
        public UpdateProcedureValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
