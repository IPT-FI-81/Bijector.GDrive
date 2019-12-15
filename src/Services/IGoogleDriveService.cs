using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;

namespace Bijector.GDrive.Services
{
    public interface IGoogleDriveServices
    {
        Task<IEnumerable<File>> GetFileAsync();

        Task<IEnumerable<File>> GetDirectoryAsync();

        Task<IEnumerable<File>> GetFileAsync(Func<File, bool> predicate);

        Task<IEnumerable<File>> GetDirectoryAsync(Func<File, bool> predicate);

        Task<IEnumerable<File>> GetFileAsync(string search);

        Task<IEnumerable<File>> GetDirectoryAsync(string search);

        Task<bool> ReName(string fileId, string newName);

        Task<bool> Move(string fileId, string destinationFolder);

        Task<bool> CreateDirectory(string parent, string name);
    }
}