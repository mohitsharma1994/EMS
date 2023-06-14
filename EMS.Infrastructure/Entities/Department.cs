﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Entities
{
    public class Department
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}
