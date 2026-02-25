using CicloSustentavel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicloSustentavel.Domain.Security.Tokens
{
    public interface IAccesTokenGenerator
    {
        string GenerateToken(UserModel user);
    }
}
