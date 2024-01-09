using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
namespace LibraryManagementSystem.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        public UnauthorizedResult Result { get; private set; }

        Task IAsyncAuthorizationFilter.OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

       public async Task OnAuthorizationAsync(AuthorizationFilter context){
        if (!(await IsAdminAuthenticated(context))){
            context.Result=new UnauthorizedResult();
        }
       }

        private Task<bool> IsAdminAuthenticated(AuthorizationFilter context)
        {
            throw new NotImplementedException();
        }
    }
}
