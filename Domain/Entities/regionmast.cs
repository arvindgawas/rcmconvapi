using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class regionmast
    {
        [Key]
        public string regioncode { get; set; }
        public string regionname { get; set; }
    }
}
