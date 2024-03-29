using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}
