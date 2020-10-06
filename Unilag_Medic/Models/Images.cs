using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Unilag_Medic.Models
{
    public class Images
    {
        // public string imgPath { get; set; }
        // public string imgUniquePath { get; set; }
        // public string uploadBy { get; set; }
        // public DateTime uploadDatetime { get; set; }
        public int patientId { get; set; }

        public IList<IFormFile> file { get; set; }
    }
}
