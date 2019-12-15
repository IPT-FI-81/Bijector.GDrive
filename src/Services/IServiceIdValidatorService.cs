using System;
using System.Threading.Tasks;

namespace Bijector.GDrive.Services
{
    public interface IServiceIdValidatorService
    {
        Task<bool> IsValid(int accountId, int ServicesId);
    }
}