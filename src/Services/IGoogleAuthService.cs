using System;
using System.Threading.Tasks;
using Bijector.GDrive.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;

namespace Bijector.GDrive.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleCredential> GetCredentialAsync(int serviceId);

        Task<GoogleCredential> GetRefreshedCredentialAsync(int serviceId);

        AuthorizationCodeFlow GetAuthorizationCodeFlow();

        string GetAuthorizationRequestUrl();

        Task<Token> StoreTokenFromCode(int accountId, string code);
    }
}