using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public decimal Price { get; set; }
        public TimeSpan CompilationTime { get; set; }
        public string Reason { get; set; }

        public virtual Application Application { get; set; }

        public virtual ICollection<SparesCount> SparesCounts { get; set; }
        
    }
}
