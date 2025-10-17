using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventorySystem.Core.DTOs.Reports;

namespace InventorySystem.Web.View_Models
{
    public class ReportViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime DateTo { get; set; }
        public List<SalesReportDto>? Sales { get; set; }
        public List<PurchaseReportDto>? Purchases { get; set; }
        public List<InventoryValuationDto>? Inventory { get; set; }
    }
}
