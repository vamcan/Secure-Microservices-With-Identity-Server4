using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Config
{
    public static class MemoryConfig
    {

        public static IEnumerable<ApiScope> ApiScopes() =>

          new List<ApiScope> {

                new ApiScope("jobsApi.scope", "Jobs API") ,
                 

          };
        public static IEnumerable<ApiResource> ApiResources() =>

         new List<ApiResource> {

                new ApiResource("jobsApi", "Jobs API")
                {
                    Scopes={"jobsApi.scope"},
                    UserClaims = new List<string>() { "role"}
                },
                
         };

        public static IEnumerable<IdentityResource> IdentityResources() =>

            new List<IdentityResource> {   

                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles","User role(s)",new List<string>{"role"})

            };

        public static IEnumerable<Client> Clients() =>

           new List<Client> {

               new Client
               {
                   ClientId= "first-client",
                   ClientSecrets = new [] { new Secret("RezaOzzSecret".Sha512()) },
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                   AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, "jobsApi.scope"},

               } ,
              new Client
              {
                  ClientName ="MvcClient",
                  ClientId="mvc-client",
                  AllowedGrantTypes = GrantTypes.Code,
                  RedirectUris= new List<string> {"https://localhost:5201/signin-oidc"},
                  AllowedScopes={IdentityServerConstants.StandardScopes.OpenId,   
                      IdentityServerConstants.StandardScopes.Profile, 
                      IdentityServerConstants.StandardScopes.Address, "roles", "jobsApi.scope"},
                  ClientSecrets = {new Secret("mvc-client-secret".Sha512())},
                  RequirePkce = true,
                  RequireConsent = true,
                  PostLogoutRedirectUris =new List<string> {"https://localhost:5201/signout-callback-oidc"},
              }
           };

        public static List<TestUser> TestUsers() =>

           new List<TestUser> {

               new TestUser
               {
                   SubjectId="abc1",
                   Username ="Reza",
                   Password ="RezaPass",
                   Claims = new List<Claim>
                   {
                       new Claim ("given_name", "Reza"),
                       new Claim ("family_name", "Ozz"),
                       new Claim ("address", "123 Mars Road, Space Treck"),
                       new Claim("role", "Admin")
                   }
               } ,
                new TestUser
               {
                   SubjectId="abc2",
                   Username ="Seros",
                   Password ="SerosPass",
                   Claims = new List<Claim>
                   {
                       new Claim ("given_name", "Seros"),
                       new Claim ("family_name", "Ozz"),
                       new Claim ("address", "123 Moon Road, The Moon"),
                         new Claim("role", "Editor")
                   }
               } 
           };
    }
}
