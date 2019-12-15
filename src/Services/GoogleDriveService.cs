using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;

namespace Bijector.GDrive.Services
{
    public class GoogleDriveService : IGoogleDriveServices
    {
        private DriveService driveService;

        private readonly IGoogleAuthService authService;

        private readonly Guid serviceId;

        public GoogleDriveService(Guid serviceId, IGoogleAuthService authService)
        {                        
            InitAsync(serviceId);                    
            this.serviceId = serviceId;
            this.authService = authService;
        }

        private async void InitAsync(Guid serviceId)
        {
            var credential = await authService.GetCredentialAsync(serviceId);
            driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Bijector.GDrive"
            });
        }


        public async Task<File> Get(string id)
        {
            var request = driveService.Files.Get(id);
            return await ExecuteAsync(request);
        }

        public async Task<bool> CreateDirectory(string parentId, string name)
        {
            File directory = new File
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new string[] { parentId }
            };
            var request = driveService.Files.Create(directory);
            if(await ExecuteAsync(request) != null)
                return true;
            return false;
        }

        public async Task<IEnumerable<File>> GetDirectoryAsync()
        {
            var request = driveService.Files.List();            
            request.Q = "trashed = false and mimeType = application/vnd.google-apps.folder";
            return await ExecuteListAsync(request);
        }

        public async Task<IEnumerable<File>> GetDirectoryAsync(Func<File, bool> predicate)
        {            
            var allDirectory = await GetDirectoryAsync();
            return allDirectory.Where(predicate);
        }

        public async Task<IEnumerable<File>> GetDirectoryAsync(string search)
        {
            var request = driveService.Files.List();            
            request.Q = $"trashed = false and mimeType = application/vnd.google-apps.folder and name contains {search}";
            return await ExecuteListAsync(request);
        }

        public async Task<IEnumerable<File>> GetFileAsync()
        {            
            var request = driveService.Files.List();            
            request.Q = "trashed = false and mimeType != application/vnd.google-apps.folder";
            return await ExecuteListAsync(request);
        }        

        public async Task<IEnumerable<File>> GetFileAsync(Func<File, bool> predicate)
        {
            var allFiles = await GetFileAsync();
            return allFiles.Where(predicate);
        }

        public async Task<IEnumerable<File>> GetFileAsync(string search)
        {
            var request = driveService.Files.List();
            request.Corpora = "user";
            request.Q = $"trashed = false and mimeType != application/vnd.google-apps.folder and name contains {search}";
            return await ExecuteListAsync(request);
        }

        public async Task<bool> Move(string id, string destinationFolderId, string sourceFolderId)
        {            
            var request = driveService.Files.Update(new File(), id);
            request.AddParents = destinationFolderId;
            request.RemoveParents = sourceFolderId;            
            if(await ExecuteAsync(request) != null)
                return true;
            return false;
        }

        public async Task<bool> ReName(string id, string newName)
        {
            var body = new File
            {                
                Name = newName
            };
            var request = driveService.Files.Update(body, id);
            request.Fields = "name";
            if(await ExecuteAsync(request) != null)
                return true;
            return false;
        }

        private async Task<File> ExecuteAsync(DriveBaseServiceRequest<File> request)
        {
            try
            {
                return await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                //log to-do
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        private async Task<IEnumerable<File>> ExecuteListAsync(FilesResource.ListRequest request)
        {
            var result = new List<File>();
            do
            {
                try
                {
                    FileList files = await request.ExecuteAsync();
                    result.AddRange(files.Files);
                    request.PageToken = files.NextPageToken;
                }
                catch (Exception ex)
                {
                    //log to-do
                    Console.WriteLine("An error occurred: " + ex.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));
            return result;
        }
        
        private async Task RefreshDriveService()
        {
            var credential = await authService.GetRefreshedCredentialAsync(serviceId);
            driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Bijector.GDrive"
            });
        }
    }
}