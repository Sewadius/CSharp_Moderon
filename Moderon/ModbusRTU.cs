using Newtonsoft.Json.Linq;
using System;
using System.IO.Ports;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderon
{
    class ModbusRTU
    {
        public SerialPort mySp = new SerialPort();
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int Timeout { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public byte Address { get; set; }

        public ModbusRTU(string port, int baud, int bits, int timeout, Parity parity, StopBits stopBits, byte address)
        {
            PortName = port;
            BaudRate = baud;
            DataBits = bits;
            Timeout = timeout;
            Parity = parity;
            StopBits = stopBits;
            Address = address;
        }

        public ModbusRTU() { }

        ///<summary>Инициализация соединения по CAN порту</summary>
        public void StartSession()
        {
            // Установка параметров для CAN порта
            mySp.PortName = PortName;
            mySp.BaudRate = BaudRate;
            mySp.DataBits = DataBits;
            mySp.Parity = Parity;
            mySp.StopBits = StopBits;

            try
            {
                mySp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///<summary>Инициализация разрыва соединения по CAN порту</summary>
        public void StopSession()
        {
            int timeout = 200; // 200 ms timeout

            try
            {
                // Create a CancellationTokenSource with timeout
                using CancellationTokenSource cts = new(timeout);
                Task sendTask = Task.Run(() =>
                    mySp.Close());

                // Wait for the task to complete or timeout to occur
                if (sendTask.Wait(timeout))
                {
                    return;
                }
                else
                {
                    cts.Cancel();
                    return;
                }
            }
            catch { return; }
        }

        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        public bool SendFc3(byte address, ushort start, ushort registers, ref short[] values)
        {
            // Ensure port is open:
            if (mySp.IsOpen)
            {
                // Clear in/out buffers:
                mySp.DiscardOutBuffer();
                mySp.DiscardInBuffer();
                // Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                // Function 3 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                // Build outgoing modbus message:
                BuildMessage(address, 3, start, registers, ref message);
                // Send modbus message to Serial Port:
                try
                {
                    mySp.ReadTimeout = 200;  // Set the read timeout to 200 ms
                    mySp.WriteTimeout = 200; // Set the write timeout to 200 ms
                    mySp.Write(message, 0, message.Length);
                                  
                    // Attempt to read the response
                    GetResponse(ref response);
                }
                catch
                {
                    // mess1.Text = "Error in read event: " + err.Message;
                    return false;
                }
                // Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 5) / 2; i++)
                    {
                        values[i] = response[2 * i + 3];
                        values[i] <<= 8;
                        values[i] += response[2 * i + 4];
                    }
                    // mess1.Text = "Read successful";
                    return true;
                }
                else
                {
                    // mess1.Text = "CRC error";
                    return false;
                }
            }
            else
            {
                // mess1.Text = "Serial port not open";
                return false;
            }
        }

        public bool SendFc1(byte address, ushort start, ushort registers, ref short[] values)
        {
            // Ensure port is open:
            if (mySp.IsOpen)
            {
                // Clear in/out buffers:
                mySp.DiscardOutBuffer();
                mySp.DiscardInBuffer();
                // Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                // Function 3 response buffer:
                ushort AnswerData = (ushort)(registers / 8);
                float fl = (float)registers / 8;
                if (fl > AnswerData) { AnswerData++; }
                byte[] response = new byte[5 + AnswerData];
                // Build outgoing modbus message:
                BuildMessage(address, 1, start, registers, ref message);
                // Send modbus message to Serial Port:
                try
                {
                    mySp.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch
                {
                    // mess1.Text = "Error in read event: " + err.Message;
                    return false;
                }
                // Evaluate message:
                if (CheckResponse(response))
                {
                    // Return requested register values:
                    for (int i = 0; i < (response.Length - 5); i++)
                    {
                        values[i] = response[i + 3];
                    }
                    // mess1.Text = "Read successful";
                    return true;
                }
                else
                {
                    // mess1.Text = "CRC error";
                    return false;
                }
            }
            else
            {
                // mess1.Text = "Serial port not open";
                return false;
            }

        }

        // Function write single register
        public bool WriteFc6(byte address, ushort start, ushort registers, ref short[] values)
        // адрес устр-ва, адрес переменной, значение переменной !
        {
            // Ensure port is open:
            if (mySp.IsOpen)
            {
                // Function 6 request is always 8 bytes:
                byte[] message = new byte[8];
                // Function 6 response buffer:             
                byte[] response = new byte[8];
                try
                {
                    // Clear in/out buffers:
                    mySp.DiscardOutBuffer();
                    mySp.DiscardInBuffer();
                    // Build outgoing modbus message:
                    BuildMessage(address, 6, start, registers, ref message);
                    // Send modbus message to Serial Port:

                    mySp.Write(message, 0, message.Length);
                    Thread.Sleep(150);   // Was 100 ms
                    GetResponse(ref response);
                }
                catch
                {
                    // return "Error in read event: " + err.Message;
                    return false;
                }
                // Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 6); i++)
                    {
                        values[i] = response[i + 4];

                    }
                    /*for (int i = 0; i < (response.Length - 4); i += 2)
                    {
                        values[i / 2] = BitConverter.ToInt16(response, i + 4);
                    } */
                    // return "Read successful";
                    return true;
                }
                else
                {
                    // return "CRC error";
                    return false;
                }
            }
            else
            {
                // return "Serial port not open";
                return false;
            }
        }

        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            // Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }

        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        private void GetResponse(ref byte[] response)
        {
            for (int i = 0; i < response.Length; i++)
            {

                response[i] = (byte)mySp.ReadByte();
                // MessageBox.Show(response[i].ToString());
            }
        }
    }
}
