﻿using Core.Entities;
using Core.Params;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        #region Constructor
        public UserSpecification(UserSpecParams specParams) : base(x =>
            (
                string.IsNullOrEmpty(specParams.Search) ||
                x.FullName.ToLower().Contains(specParams.Search) ||
                x.Address.ToLower().Contains(specParams.Search)
            )
        &&
            (
                specParams.Roles.Count == 0 ||
                x.UserRoles.Any(ur => specParams.Roles.Contains(ur.Role!.Name))
            )
        )
        {
            AddInclude(x => x.Include(u => u.UserRoles).ThenInclude(ur => ur.Role!));

            AddInclude(x => x.Include(u => u.Followers));

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        #endregion
    }
}