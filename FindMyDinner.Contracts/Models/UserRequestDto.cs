using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyDinner.Contracts.Models
{
    public class UserRequestDto
    {
    
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public int  modilenumber { get; set; }

    }
}
