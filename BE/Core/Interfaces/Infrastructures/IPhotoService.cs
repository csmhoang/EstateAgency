﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Core;

public interface IPhotoService
{
    Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
