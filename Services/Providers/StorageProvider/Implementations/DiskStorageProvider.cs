using System;
using System.IO;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Services.Providers.StorageProvider.Implementations {
  public class DiskStorageProvider : StorageProvider {
    private readonly IHostEnvironment hostEnvironment;
    public readonly string PokemonPhotosFolder;

    public DiskStorageProvider(IHostEnvironment hostEnvironment) {
      this.hostEnvironment = hostEnvironment;

      PokemonPhotosFolder = Path.Combine(
        this.hostEnvironment.ContentRootPath,
        "wwwroot",
        "images"
      );
    }

    public string SaveFile(IFormFile file) {
      if (!Directory.Exists(PokemonPhotosFolder))
        Directory.CreateDirectory(PokemonPhotosFolder);

      using FileStream uploadedPhoto = File.Create(
        Path.Combine(
          PokemonPhotosFolder,
          Guid.NewGuid().ToString()
          + Path.GetExtension(file.FileName)
        )
      );
      file.CopyTo(uploadedPhoto);
      uploadedPhoto.Flush();

      return Path.GetFileName(uploadedPhoto.Name);
    }

    public void DeleteFile(string filename) {
      File.Delete(
        Path.Combine(
          PokemonPhotosFolder,
          filename
        )
      );
    }
  }
}
