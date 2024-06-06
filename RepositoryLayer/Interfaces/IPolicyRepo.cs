using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IPolicyRepo
    {
        Task<bool> AddPolicy(PolicyEntity policy);
        Task<IEnumerable<PolicyEntity>> getAllPolicies();
    }
}
