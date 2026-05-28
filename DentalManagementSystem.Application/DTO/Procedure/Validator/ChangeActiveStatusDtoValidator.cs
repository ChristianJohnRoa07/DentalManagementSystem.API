using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Procedure.Validator
{
    public class ChangeActiveStatusDtoValidator : AbstractValidator<ChangeActiveStatusDto>
    {
        public ChangeActiveStatusDtoValidator()
        {
            RuleFor(p => p.ProcedureId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
