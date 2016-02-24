using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Helpers;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using XplicitApp.Infrastracture.Utils;
using XplicitConstants;

namespace XplicitApp
{
    public class Startup
    {

        public void Configuration(IAppBuilder appBuilder)
        {
            // Enable AntiForgery token support
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityModel.JwtClaimTypes.Name;

            // Reset the mapping dictionary to ensure claim types  maps to .NET  claim class types
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

           // Sign into a client with a claim_identity that is created from the id_token , the claim_identity is stored in a cookie thus use of cookie authentication
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "Cookie",
                ExpireTimeSpan = new TimeSpan(0,30,0),
                SlidingExpiration = true // re-issue a cookie when there is request after 1/2 of the expiration window has passed
            });

            appBuilder.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions()
            {
                ClientId = "xplicitapphybrid",
                Authority = Constants.XplicitSts,
                RedirectUri = Constants.XplicitAppMvc,
                SignInAsAuthenticationType = "Cookies",

                // Request response-type thus 'code' = Authorization code, 'token' = access_token, 'id_token' = Identity token
                ResponseType = "code id_token token",
                Scope = "openid profile address xplicitmanagement roles offline_access",
                UseTokenLifetime = false, // identity_token life l'l not be used but the expiration of the authentication ticket l'l be used
                PostLogoutRedirectUri = Constants.XplicitAppMvc,

                // for token validation
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = async n =>
                    {
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.AccessToken);

                        var givenNameClaim =
                            n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);
                        var familyNameClaim =
                            n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);
                        var subClaim = n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

                        // Create a new claims, issuer + sub as unique identifier
                        var nameClaim = new Claim(
                            IdentityModel.JwtClaimTypes.Name,
                            Constants.XplicitAppIssuerUri,
                            subClaim.Value
                            );

                        var newClaimIdentity = new ClaimsIdentity(

                            n.AuthenticationTicket.Identity.AuthenticationType,
                            IdentityModel.JwtClaimTypes.Name,
                            IdentityModel.JwtClaimTypes.Role
                            
                            );

                        if (nameClaim != null)
                        {
                            newClaimIdentity.AddClaim(nameClaim);
                        }

                        if (givenNameClaim != null)
                        {
                            newClaimIdentity.AddClaim(givenNameClaim);
                        }

                        if (familyNameClaim != null)
                        {
                            newClaimIdentity.AddClaim(familyNameClaim);
                        }

                        var tokenClientForRefreshToken = new TokenClient(
                           Constants.XplicitStsTokenEndpoint,
                           "xplicitapphybrid",
                           Constants.XplicitClientSecret
                           );

                        var refreshResponse = await tokenClientForRefreshToken.RequestAuthorizationCodeAsync(
                            n.ProtocolMessage.Code,
                            Constants.XplicitAppMvc
                            );

                        // saving the refresh token
                        var expirationDateAsRoundAppString =
                            DateTime.SpecifyKind(DateTime.UtcNow.AddSeconds(refreshResponse.ExpiresIn), DateTimeKind.Utc)
                                .ToString("o");

                        // add tokens to be accessed it later
                        newClaimIdentity.AddClaim(new Claim("access_token", refreshResponse.AccessToken));
                        newClaimIdentity.AddClaim(new Claim("refresh_token", refreshResponse.RefreshToken));
                        newClaimIdentity.AddClaim(new Claim("expires_at", expirationDateAsRoundAppString));
                        newClaimIdentity.AddClaim(new Claim("id_token", refreshResponse.IdentityToken));

                        // Create a new Authentication ticket, overwriting the old one.
                        n.AuthenticationTicket = new AuthenticationTicket(newClaimIdentity, n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = async n =>
                    {
                        // get id token to add id token hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var identityTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (identityTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = identityTokenHint.Value;
                            }
                        }
                    }
                }
            });
        }

    }
}