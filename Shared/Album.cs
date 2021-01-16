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
        public string UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AlbumID { get; set; }
        public int ImageCount { get; set; }
        public string AlbumName { get; set; }
    }
}
