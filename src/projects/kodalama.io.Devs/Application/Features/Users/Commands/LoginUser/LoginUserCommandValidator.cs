using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x=>x.UserForLoginDto.Email).NotEmpty();
            RuleFor(x=>x.UserForLoginDto.Password).NotEmpty();
            RuleFor(x => x.UserForLoginDto.Email).EmailAddress();
            //RuleFor(x=>x.UserForLoginDto.AuthenticatorCode).NotEmpty();
        }
    }
}
