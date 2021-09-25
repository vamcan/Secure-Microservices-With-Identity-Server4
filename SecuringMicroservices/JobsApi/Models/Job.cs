using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

        public DateTime PostedDate { get; set; }
        public string Location { get; set; }
    }
}
