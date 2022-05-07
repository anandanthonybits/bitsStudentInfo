using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsUserDirectory.Models
{
    public class StudentInfo
    {
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Roll No.")]
        public string rollNo { get; set; }

        [DisplayName("Alias")]
        public string shortUrl { get; set; }
    }
}