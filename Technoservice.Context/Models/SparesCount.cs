using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class SparesCount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public int SparesTypeId { get; set; }

        public virtual SparesType SparesType { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
