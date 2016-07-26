using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Modbus;
using Modbus.Device;
using System.Net.Sockets;
using System.Net.NetworkInformation;


namespace ComToTCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
        }
        public static ModbusIpMaster master;
        public static TcpClient tcp;
        public static bool connected = false;
        public static string IPaddress;
        public static ushort[] value_array = new ushort[0];
 
        public static float[] res_array1 = new float[0];

        public static byte[] bytes;
        public static ushort[] floating = new ushort[1000];

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;

            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string ports = comboBox1.SelectedItem.ToString();
            string BaudRate = comboBox2.SelectedItem.ToString();
            string Databits = comboBox3.SelectedItem.ToString();
            string Parity_ = comboBox4.SelectedItem.ToString();
            string Stopbits_ = comboBox5.SelectedItem.ToString();
           



            try
            {
                System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort(ports);


                port.BaudRate = Convert.ToInt32(BaudRate);
                port.DataBits = Convert.ToInt32(Databits);

                if (Parity_ == "Even") { port.Parity = System.IO.Ports.Parity.Even; }
                else if (Parity_ == "Odd") { port.Parity = System.IO.Ports.Parity.Odd; }
                else if (Parity_ == "None") { port.Parity = System.IO.Ports.Parity.None; }
                else if (Parity_ == "Mark") { port.Parity = System.IO.Ports.Parity.Mark; }
                else { port.Parity = System.IO.Ports.Parity.Space; }

                if (Stopbits_ == "1") { port.StopBits = System.IO.Ports.StopBits.One; }
                else if (Stopbits_ == "1.4") { port.StopBits = System.IO.Ports.StopBits.OnePointFive; }
                else { port.StopBits = System.IO.Ports.StopBits.Two; }

                port.Open();

                ModbusSerialMaster Sermaster = ModbusSerialMaster.CreateRtu(port);

                ushort[] value_array = Sermaster.ReadHoldingRegisters(Convert.ToByte(textBox1.Text), 2421, 100);
                res_array1 = RegisterConvertor.ToSingleArray(value_array);
                port.Close();
                
            }
            catch (Exception ex)
            {

            }


            System.Net.IPAddress[] localIPs = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    // Console.WriteLine(addr);
                    IPaddress = addr.ToString();

                }
            }


            if (GloablClass.connected == 0)
            {
                byte[] addr;
                addr = System.Net.IPAddress.Parse(IPaddress).GetAddressBytes();

                GloablClass.CreateSlave(addr);

                if (!GloablClass.PingIp(IPaddress)) return;

                if (!GloablClass.CreateMaster(IPaddress)) return;

                label1.ForeColor = Color.Green;

            }

            try
            {
                Int32 a=2421; Int32 b=2422;
                foreach (float s in res_array1)
                {

                    bytes = BitConverter.GetBytes(Convert.ToSingle(s));

                    floating[0] = BitConverter.ToUInt16(bytes, 0);
                    floating[1] = BitConverter.ToUInt16(bytes, 2);
                    GloablClass.slave.DataStore.HoldingRegisters[a] = floating[1];
                    GloablClass.slave.DataStore.HoldingRegisters[b] = floating[0];
                    a=a+2;
                    b=b+2;
                }

                    D1.Text = res_array1[0].ToString();
                    D2.Text = res_array1[1].ToString();
                    D3.Text = res_array1[2].ToString();
                    D4.Text = res_array1[3].ToString();
                    D5.Text = res_array1[4].ToString();
                    D6.Text = res_array1[5].ToString();
                    D7.Text = res_array1[6].ToString();
                    D8.Text = res_array1[7].ToString();
                    D9.Text = res_array1[8].ToString();
                    D10.Text = res_array1[9].ToString();
                    D11.Text = res_array1[10].ToString();
                    D12.Text = res_array1[11].ToString();
                    D13.Text = res_array1[12].ToString();
                    D14.Text = res_array1[13].ToString();
                    D15.Text = res_array1[14].ToString();
                    D16.Text = res_array1[15].ToString();
                    D17.Text = res_array1[16].ToString();
                    D18.Text = res_array1[17].ToString();
                    D19.Text = res_array1[18].ToString();
                    D20.Text = res_array1[19].ToString();
                    D21.Text = res_array1[20].ToString();
                    D22.Text = res_array1[21].ToString();
                    D23.Text = res_array1[22].ToString();
                    D24.Text = res_array1[23].ToString();
                    D25.Text = res_array1[24].ToString();
                    D26.Text = res_array1[25].ToString();
                    D27.Text = res_array1[26].ToString();
                    D28.Text = res_array1[27].ToString();
                    D29.Text = res_array1[28].ToString();
                    D30.Text = res_array1[29].ToString();
                    D31.Text = res_array1[30].ToString();
                    D32.Text = res_array1[31].ToString();
               
                   
                
               
            }
            catch (Exception ex)
            {

            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }



    }
}
