using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net.NetworkInformation;

using Modbus;
using Modbus.Extensions;
using Modbus.Device;


namespace ComToTCP
{
    class command_variable
    {
    }

    public class GloablClass
    {
        public static string IPaddress;
        public static int connected = 0;
        public static ModbusIpMaster master;
        public static TcpClient tcp;

        public static TcpListener listener;
        public static ModbusSlave slave;

        public static bool WflowMeter = false;
        public static bool WpressureMeter = false;
        public static bool OflowMeter = false;
        public static bool OpressureMeter = false;
        public static bool MflowMeter = false;
        public static bool MpressureMeter = false;
        public static bool A_1FlowMeter = false;
        public static bool ApressureMeter = false;
        public static bool A_2FlowMeter = false;
        public static bool A_3FlowMeter = false;
           
        public static bool PingIp(string ipAddress)
        {
            Ping ping = new Ping();
            int counter = 0;

            for (counter = 0; counter < 3; counter++)
            {
                PingReply reply = ping.Send(ipAddress);
                if (reply.Status == IPStatus.TimedOut)
                {
                    GloablClass.connected = 0;
                    MessageBox.Show(ipAddress + " Connectivity Poor!", "Error");
                    return false;
                }
            }
            return true;
        }

        public static bool CreateMaster(string ipAddress)
        {
            try
            {
                GloablClass.tcp = new TcpClient(ipAddress, 502);
                GloablClass.master = ModbusIpMaster.CreateIp(tcp);
                connected = 1;
                return true;
            }
            catch (Exception ex)
            {
                connected = 0;
                MessageBox.Show(ipAddress + " ModBus Connection Wrong!", "Error");
                return false;
            }
        }

        public static bool CreateSlave(byte[] slaveAdderss)
        {
            System.Net.IPAddress ip = new System.Net.IPAddress(slaveAdderss);
            listener = new TcpListener(ip, 502);
            slave = ModbusTcpSlave.CreateTcp(1, listener);
            slave.Listen();

            return true;
        }



    }
}
