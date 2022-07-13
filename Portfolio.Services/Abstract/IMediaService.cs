using Portfolio.Entity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Abstract
{
    public interface IMediaService
    {
        string GetMediaUrl(Media media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(Media media);

        Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task DeleteMediaAsync(Media media);

        Task DeleteMediaAsync(string fileName);
    }
}
