using HealthCheck.ExceptionHandler;
using HealthCheck.Mapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HealthCheck.Service
{
    public class BaseService
    {
        protected IHttpContextAccessor _httpContextAccessor;
        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public T Map<T>(object source) where T : class => MapContext<T>.Map(source);
        public string GetClaim(string claim) => _httpContextAccessor.HttpContext.User.FindFirst(claim).Value;
        public string ActiveAccount => GetClaim("ActiveAccount");
        public string UserEmail => GetClaim("email");
        protected string UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        protected IIdentity Identity => _httpContextAccessor.HttpContext.User.Identity;

        public void ThrowNotFoundException(Guid id)
        {
            throw new NotFoundException($"Record Not Found : {id}");
        }
    }
}
