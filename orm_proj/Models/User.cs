using Microsoft.EntityFrameworkCore.Metadata;
using orm_proj.Models.Common;

namespace orm_proj.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
