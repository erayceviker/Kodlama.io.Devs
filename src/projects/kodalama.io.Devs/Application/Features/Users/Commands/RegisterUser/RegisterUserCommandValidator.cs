using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserForRegisterDto.Email).EmailAddress();
            RuleFor(x=>x.UserForRegisterDto.Email).NotEmpty();
            RuleFor(x=>x.UserForRegisterDto.Password).NotEmpty();
            RuleFor(x=>x.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.LastName).NotEmpty();
        }
    }
}
