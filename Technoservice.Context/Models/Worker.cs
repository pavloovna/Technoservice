using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string FIO { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public override string ToString()
        {
            return $"{FIO}";
        }
    }
}
