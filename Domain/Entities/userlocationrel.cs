using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class userlocationrel
    {
        public string  UserID { get; set; }
        [Key]        
        public string LocationCode { get; set; }
    }
}

