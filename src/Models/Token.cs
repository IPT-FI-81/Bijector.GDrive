using System;
using Bijector.Infrastructure.Types;
using Google.Apis.Auth.OAuth2.Responses;

namespace Bijector.GDrive.Models
{
    public class Token : TokenResponse, IIdentifiable
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
    }
}