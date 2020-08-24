using Microsoft.AspNetCore.Http;

namespace Services.Providers.StorageProvider {
  public interface StorageProvider {
    public string SaveFile(IFormFile file);
    public void DeleteFile(string filename);
  }
}
