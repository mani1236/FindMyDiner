namespace FindMyDinner.Contracts.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName ="nvarchar(100)")]
        public string FullName { get; set; }
    }
}
