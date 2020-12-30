using YouTube.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTube.Authorization
{
    public class VideoAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Video>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public VideoAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Video resource)
        {
            var applicationUser = await userManager.GetUserAsync(context.User);

            if((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name) && applicationUser == resource.Creator)
            {
                context.Succeed(requirement);
            }

            if(requirement.Name == Operations.Read.Name && !resource.Published && applicationUser == resource.Creator)
            {
                context.Succeed(requirement);
            }
        }
    }
}
