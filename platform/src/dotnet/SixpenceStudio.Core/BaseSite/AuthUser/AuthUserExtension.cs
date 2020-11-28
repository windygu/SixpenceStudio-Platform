﻿using SixpenceStudio.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.AuthUser
{
    public static class AuthUserExtension
    {
        public static CurrentUserModel ToCurrentUserModel(this auth_user user)
        {
            return new CurrentUserModel()
            {
                Code = user.code,
                Id = user.Id,
                Name = user.name
            };
        }
    }
}