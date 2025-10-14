using System.ComponentModel.DataAnnotations;

namespace InventorySystem.web.View_Models
{
    public class ReportViewModel
    {
        [DataType(DataType.DateTime)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTo { get; set; }
    }
}
