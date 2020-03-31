using System;
using System.Collections.Generic;

namespace FindMydinner.Domain.Model.Entities
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
    }
}
