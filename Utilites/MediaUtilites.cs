using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilites
{
    public class MediaUtilites
    {
        //notes path must inclue media exctention e.g filePath = "path/to/save/image.jpg";
        public async Task SaveMediaAsync(byte[] mediaBytes, string filePath)
        {
            await File.WriteAllBytesAsync(filePath, mediaBytes);
        }

        public async Task SaveMediaAsync(Stream mediaStream, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await mediaStream.CopyToAsync(fileStream);
            }
        }
    }
}
