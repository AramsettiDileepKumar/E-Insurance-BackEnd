using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PaymentEntity
    {
            public int PaymentId { get; set; }
            public DateTime PaymentDate { get; set; }
            public string Status { get; set; }
            public decimal PremiumAmount { get; set; }       
            public string PaymentMethod { get; set; }
          public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public int AgentId { get; set; }
        public decimal AnnualIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long MobileNumber { get; set; }
        public string? Address { get; set; }
    }
}
