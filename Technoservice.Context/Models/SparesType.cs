﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technoservice.Context.Models
{
    public class SparesType
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<SparesCount> SparesCounts { get; set; }
        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
