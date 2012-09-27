using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.NexHR.Models
{
    public class Department : EntityBase<int>
    {        
        public Employee Manager { get; set; }
    }
}
