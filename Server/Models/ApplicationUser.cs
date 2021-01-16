using ImageRepository.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepository.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ImageCount { get; set; }
    }
}
