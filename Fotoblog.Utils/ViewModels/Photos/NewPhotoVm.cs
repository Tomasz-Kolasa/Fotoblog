using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoblog.Utils.ViewModels.Photos
{
    public class NewPhotoVm
    {
        public string Title { get; set; } = string.Empty;
        public string ? Description { get; set; }
        public string FileBase64 { get; set; } = string.Empty;
        public int[] ? Tags { get; set; }
    }
}