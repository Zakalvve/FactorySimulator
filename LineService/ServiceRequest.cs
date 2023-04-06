using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBearPlastics
{
    public class ServiceRequest
    {
        public ServiceRequest(RequestType rtype, ICommand command, int machineId) {
            rType = rtype;
            Command = command;
            MachineID = machineId;
            ReduceType();
        }
        private void ReduceType() {
            switch (rType) {
                case RequestType.REPLACE_INPUT:
                    ResponseTime = 13 * 60;
                    Name = "Replace input container";
                    break;
                case RequestType.REPLACE_OUTPUT:
                    ResponseTime = 7 * 60;
                    Name = "Replace output container";
                    break;
                case RequestType.REPLACE_SCRAP:
                    ResponseTime = 5 * 60;
                    Name = "Replace scrap container";
                    break;
                default:
                    break;
            }
        }
        public RequestType rType { get; set; }
        public int ResponseTime { get; set; } = 0;
        public string Name { get; set; } = "";
        public ICommand Command { get; set; }

        public int MachineID { get; set; }
    }

    public enum RequestType {
        REPLACE_INPUT,
        REPLACE_OUTPUT,
        REPLACE_SCRAP
    }
}
