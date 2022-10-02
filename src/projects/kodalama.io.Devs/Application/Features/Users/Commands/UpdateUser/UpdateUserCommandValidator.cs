using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.updateUserBodyDto.FirstName).NotEmpty().WithMessage("First Name Boş olmamalı.");
            RuleFor(x=>x.updateUserBodyDto.LastName).NotEmpty().WithMessage("Last Name Boş Olmamalı.");
        }
    }
}
