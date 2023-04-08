using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBearPlastics
{
    public class ServicerPerformanceModel
    {
        public ServicerPerformanceModel(double avUptime, double currWorkMins) {
            AverageUptime = avUptime;
            CurrentWorkInMinutes = currWorkMins;
        }
        public double AverageUptime { get; set; }
        public double CurrentWorkInMinutes { get; set; }
    }
}
