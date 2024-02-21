using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HRApplicationAPI.Core.Data.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserValidateRepository repository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock,
            IUserValidateRepository repository) :
            base(options, logger, encoder, clock)
        {
            this.repository = repository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string userName = "";
            try
            {
                var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter ?? "")).Split(":");
                userName = credentials[0] ?? "";
                var password = credentials[1];


                if (!await repository.isValidateUser(userName, password ?? String.Empty))
                    throw new ArgumentException("Invalid Credentials");

            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication Falied {ex.Message}");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, userName) };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
