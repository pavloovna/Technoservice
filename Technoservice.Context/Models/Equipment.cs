using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int SerialNumber { get; set; }
        public int EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
