using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class hublocationmast
    {
        [Key]
        public string hublocationcode { get; set; }
        public string hublocationname { get; set; }

    }
}


