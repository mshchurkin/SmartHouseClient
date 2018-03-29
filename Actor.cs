using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseClient
{
    public class Actor
    {
        public String id { get; set; }

        public String houseId { get; set; }

        public int value { get; set; }

        public String actorType { get; set; }
    }
}
