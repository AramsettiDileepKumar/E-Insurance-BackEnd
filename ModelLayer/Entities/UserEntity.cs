using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Location { get; set; }
    }
}
