using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PurchaseEntity
    { 
            public int PurchaseId { get; set; }
            public int CustomerId { get; set; }
            public int PolicyId { get; set; }
            public int AgentId { get; set; }
            public DateTime PurchaseDate { get; set; }
    }
}
