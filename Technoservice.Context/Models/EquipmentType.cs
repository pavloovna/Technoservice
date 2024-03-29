using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
