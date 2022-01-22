using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Entities
{
    public class latlongreport
    {
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string user { get; set; }
        public string region { get; set; }
        public string location { get; set; }
    }
}


