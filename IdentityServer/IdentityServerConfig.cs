using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<Client> Clients => new Client[] { 
            new Client
            {
                ClientId = "MoviesClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "MoviesApi" }
            },
            new Client
            {
                ClientId = "movies_mvc_client",
                ClientName = "Movies MVC Web App",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                AllowRememberConsent = false,
                RedirectUris = new List<string>()
                {
                    "https://localhost:5002/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>()
                {
                    "https://localhost:5002/signout-callback-oidc"
                },
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    "MoviesApi",
                    "roles"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { new ApiScope("MoviesApi", "Movies Api") };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Email(),
            new IdentityResource(
                "roles",
                "Your role(s)",
                new List<string>() { "role" })
        };

        public static List<TestUser> TestUsers => new() 
        {
            new TestUser
            {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "saad",
                Password = "123qweASD",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, "saad"),
                    new Claim(JwtClaimTypes.FamilyName, "qais")
                }
            }
        };
    }
}
