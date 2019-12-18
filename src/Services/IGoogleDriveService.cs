using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;

namespace Bijector.GDrive.Services
{
    public interface IGoogleDriveServices
    {
        Task<File> Get(string id);

        Task<IEnumerable<File>> GetFilesAsync();

        Task<IEnumerable<File>> GetDirectoryAsync();

        Task<IEnumerable<File>> GetFilesAsync(Func<File, bool> predicate);

        Task<IEnumerable<File>> GetDirectoryAsync(Func<File, bool> predicate);

        Task<IEnumerable<File>> GetFilesAsync(string search);

        Task<IEnumerable<File>> GetDirectoryAsync(string search);

        Task<bool> ReName(string Id, string newName);

        Task<bool> Move(string Id, string destinationFolderId);

        Task<bool> CreateDirectory(string parentId, string name);
    }
}