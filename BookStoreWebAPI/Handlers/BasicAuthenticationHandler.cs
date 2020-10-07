using BookStoreWebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BookStoreWebAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BookStoresContext _context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, BookStoresContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail("Authroization Header Not Found");
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credientials = Encoding.UTF8.GetString(bytes).Split(":");

                string emailAddress = credientials[0];
                string password = credientials[1];

                User user = _context.Users.Where(e => e.EmailAddress == emailAddress & e.Password == password).FirstOrDefault();

                if (user == null)
                {
                    AuthenticateResult.Fail("Invalid UserName and Password");
                }
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.EmailAddress) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var tickets = new AuthenticationTicket(principal, Scheme.Name);
                  return  AuthenticateResult.Success(tickets);

                }

            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Error has occured");
            }

            return AuthenticateResult.Fail("");
        }
    }
}
