using Portfolio.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Portfolio.Services.Concrete
{
    public class LocalStorageService : IStorageService
    {
        private const string MediaRootFoler = "user-content";

        public Task DeleteMediaAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }

        public async Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }
        }
    }
}
