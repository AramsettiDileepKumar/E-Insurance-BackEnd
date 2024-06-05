using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.Registration
{
    public class UserLoginRequest
    {
        public string? EmailId { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
