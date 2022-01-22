using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;



namespace Domain.Entities
{
  
    public class UserMaster
    {
        public Int32 UserId { get; set; }
        [Key]
        public string LoginID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
    }
}
