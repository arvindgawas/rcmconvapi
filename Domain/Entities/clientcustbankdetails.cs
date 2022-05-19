using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class clientcustbankdetails
    {
        [Key]
        public string clientcustcode { get; set; }
        public string depositiontype { get; set; }
    }
}
