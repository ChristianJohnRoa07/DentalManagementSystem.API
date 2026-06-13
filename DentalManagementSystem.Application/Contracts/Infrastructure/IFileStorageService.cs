using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Contracts.Infrastructure
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        void DeleteFile(string filePath);
    }
}
