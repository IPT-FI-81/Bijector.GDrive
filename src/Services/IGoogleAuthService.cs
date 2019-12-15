using System;
using System.Threading.Tasks;
using Bijector.GDrive.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;

namespace Bijector.GDrive.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleCredential> GetCredentialAsync(Guid serviceId);

        Task<GoogleCredential> GetRefreshedCredentialAsync(Guid serviceId);

        AuthorizationCodeFlow GetAuthorizationCodeFlow();

        string GetAuthorizationRequestUrl();

        Task<Token> StoreTokenFromCode(Guid accountId, string code);
    }
}