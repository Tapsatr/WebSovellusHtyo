using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Models
{
    public enum Status
    {
        ORDERED, STARTED, READY, ACCEPTED, REJECTED
    }
    public class JobOrder
    {
        public int ID { get; set; }
        public string Orderer { get; set; }
        [Required]
        public string JobDescription { get; set; }
        public Status Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime OrderDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ReadyDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? AcceptedOrderDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? RejectedOrderDate { get; set; }

        public string JobComment { get; set; }
        public string ToolsComment { get; set; }
        public double HoursOnJob { get; set; }
        public double PriceEstimate { get; set; }
    }
}
