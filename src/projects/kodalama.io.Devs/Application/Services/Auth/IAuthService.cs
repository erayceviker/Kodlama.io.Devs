using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        AccessToken CreateAccessToken(User user);
    }
}
