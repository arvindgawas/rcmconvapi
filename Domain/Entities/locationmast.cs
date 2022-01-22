using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class locationmast
    {
        [Key]
        public string locationcode { get; set; }
        public string locationname { get; set; }

        public string regioncode { get; set; }
    }
}
