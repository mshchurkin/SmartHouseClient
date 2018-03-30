using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseClient
{
    public class Sensor
    {
        public  String id { get; set; }

        public String houseId { get; set; }

        public String measurement { get; set; }

        public String fieldName { get; set; }

        public bool active { get; set; }

        public int value { get; set; }

        public String sensorType { get; set; }

        public String extreme { get; set; }
    }
}
