using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Models
{
    public class JobOrder
    {
        public int ID { get; set; }
        public string Orderer { get; set; }
        [Required]
        public string JobDescription { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ReadyDate { get; set; }

        public DateTime AcceptedOrderDate { get; set; }

        public DateTime RejectedOrderDate { get; set; }

        public string JobComment { get; set; }

        public string ToolsComment { get; set; }
        public double HoursOnJob { get; set; }

        public double PriceEstimate { get; set; }


    }
}
