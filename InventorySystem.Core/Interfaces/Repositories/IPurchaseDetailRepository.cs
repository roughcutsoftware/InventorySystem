using InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface IPurchaseDetailRepository
    {
        List<PurchaseDetails> GetPurchasesWithinDateRange(DateTime from, DateTime to);
    }
}
