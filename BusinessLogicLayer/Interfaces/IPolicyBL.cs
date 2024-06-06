using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPolicyBL
    {
        Task<bool> AddPolicy(PolicyRequest request);
        Task<IEnumerable<PolicyEntity>> getAllPolicies();
    }
}
