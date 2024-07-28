using Expenses.Application.Attributes;
using Expenses.Application.Exceptions;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Expenses.Application.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizationBehaviour(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = _contextAccessor.HttpContext;

            if (httpContext is null)
                throw new InvalidOperationException("Http Context is not avaible");

            var endpoint = httpContext.GetEndpoint();
            var authorizeAttributes = endpoint?.Metadata?.GetOrderedMetadata<AuthorizePermissionAttribute>();

            if (authorizeAttributes is not null && authorizeAttributes.Any())
            {
                var user = httpContext.User;
                if (user is null || !user.Identity.IsAuthenticated)
                    throw new UnauthorizedAccessException("Usuario no autenticado");

                var permissionsClaim = user.Claims.FirstOrDefault();
                if (permissionsClaim is null)
                    throw new UnauthorizedAccessException("Permisos no disponibles");

                var accountJson = permissionsClaim.Value;
                var account = JsonSerializer.Deserialize<AccountDTO>(accountJson);

                foreach (var authorizeAttribute in authorizeAttributes)
                {
                    if (!account.Permissions.Any(p => p.Name == authorizeAttribute.Permission))
                        throw new ForbiddenException($"No tienes permiso a: {authorizeAttribute.Permission}");
                }

            }

            return await next();
        }
    }
}
