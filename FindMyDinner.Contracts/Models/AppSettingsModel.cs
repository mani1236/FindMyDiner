using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyDinner.Contracts.Models
{
    public class AppSettingsModel
    {
        public string Jwt_Secret { get; set; }
        public string Client_URI { get; set; }
    }
}
