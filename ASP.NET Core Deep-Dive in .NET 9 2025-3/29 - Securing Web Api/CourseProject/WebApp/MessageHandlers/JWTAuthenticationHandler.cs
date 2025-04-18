using System.Net.Http.Headers;
using System.Text.Json;
using WebApp.Model;

namespace WebApp.MessageHandlers
{
    public class JWTAuthenticationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public JWTAuthenticationHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var session = httpContext?.Session;

            // Get token
            JsonWebToken token;

            string? strJWTToken = session?.GetString("access_token");
            if (string.IsNullOrWhiteSpace(strJWTToken)) 
            {
                token = await Login();
            }
            else
            {
                token = JsonSerializer.Deserialize<JsonWebToken>(strJWTToken) ?? new JsonWebToken();
            }

            // Handle token expiration
            if (token == null ||
                string.IsNullOrWhiteSpace(token.AccessToken) ||
                token.ExpiresAt <= DateTime.UtcNow) 
            {
                token = await Login();
            }

            // Send token: add the token to authorization header
            if (!string.IsNullOrWhiteSpace(token.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<JsonWebToken> Login()
        {
            // Create a new http client
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync($"{configuration["WebApi:Url"]}account/login",
                new
                {
                    ClientId = configuration["WebApi:ClientId"],
                    ClientSecret = configuration["WebApi:ClientSecret"]
                });
            response.EnsureSuccessStatusCode();

            string strJwt = await response.Content.ReadAsStringAsync();

            // Store the token in the session
            httpContextAccessor.HttpContext?.Session.SetString("access_token", strJwt);

            return JsonSerializer.Deserialize<JsonWebToken>(strJwt) ?? new JsonWebToken();
        }
    }
}
