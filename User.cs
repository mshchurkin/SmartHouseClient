using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseClient
{
    public class User
    {
        public String login { get; set; }
        public String password { get; set; }
        public String token { get; set; }

        public bool integrator { get; set; }
    }
}
