using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ClientId { get; set; }
        public int EquipmentId { get; set; }
        public int TypeBrokenId { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int StatusId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Status Status { get; set; }
        public virtual TypeBroken TypeBroken { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
