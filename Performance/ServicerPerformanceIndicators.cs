using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBearPlastics
{
    public class ServicerPerformanceIndicators : KeyPerformanceIndicators, IServicerPerformanceIndicators
    {
        public int CurrentWork { get; set; }
        public double CurrentWorkInMinutes { get => _clock.ToMinutes(CurrentWork); }

        public void Tick() {
            if (CurrentWork > 0) {
                CurrentWork--;
            }
        }
        public ServicerPerformanceModel ExtractData() {
            return new ServicerPerformanceModel(AverageUptime,CurrentWorkInMinutes);
        }
    }
}
