using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ImageRepository.Shared
{
    public class Image
    {
        public Image()
        {
        }
        public string UserID { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ImageId { get; set; }
        public byte[] Data { get; set; }
        public string Base64DisplayData { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public bool Private { get; set; }
        public string ContentType { get; set; }
    }
}
