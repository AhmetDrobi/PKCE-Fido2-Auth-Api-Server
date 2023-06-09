﻿using IdentityServer.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<QRCodeUser> _claimsFactory;
        private readonly UserManager<QRCodeUser> _userManager;

        public ProfileService(UserManager<QRCodeUser> userManager, IUserClaimsPrincipalFactory<QRCodeUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // Not doing any change here
            var user = await _userManager.GetUserAsync(context.Subject);
            ClaimsPrincipal principal = await _claimsFactory.CreateAsync(user);
            context.IssuedClaims = principal.Claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}
