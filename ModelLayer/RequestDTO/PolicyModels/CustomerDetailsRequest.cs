using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class CustomerDetailsRequest
    {
            public int PolicyId { get; set; }
            public int AgentId { get; set; }
            public decimal AnnualIncome { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public DateTime DateOfBirth { get; set; }
            public long MobileNumber { get; set; }
            public string Address { get; set; }
    }
}
