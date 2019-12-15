using System;
using System.Threading.Tasks;
using Bijector.GDrive.Models;
using Bijector.Infrastructure.Repositories;

namespace Bijector.GDrive.Services
{
    public class ServiceIdValidatorService : IServiceIdValidatorService
    {
        private readonly IRepository<Token> tokenRepository;

        public ServiceIdValidatorService(IRepository<Token> tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }

        public async Task<bool> IsValid(int accountId, int servicesId)
        {
            var token = await tokenRepository.GetByIdAsync(servicesId);
            return token.AccountId == accountId;
        }
    }
}