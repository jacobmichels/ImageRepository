using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRepository.Shared
{
    public class Album
    {
        public string OwnerID { get; set; }
        public string AlbumID { get; set; }
        public string AlbumName { get; set; }
        public string[] ImagesIDs { get; set; }
    }
}
