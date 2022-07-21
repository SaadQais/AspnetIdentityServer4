using IdentityServer4.Models;
using IdentityServer4.Test;

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
            } 
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { new ApiScope("MoviesApi", "Movies Api") };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] { };

        public static List<TestUser> TestUsers => new() { };
    }
}
