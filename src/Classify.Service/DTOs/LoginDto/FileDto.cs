using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify.Service.DTOs.LoginDto
{
    public class FileDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }

    }
}
