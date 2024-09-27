using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CCD_SOCKET_CLIENT
{
    public partial class CCD_SOCKET_CLIENT : Form
    {
        private const int DefaultPort = 4400; 
        public CCD_SOCKET_CLIENT()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            rtb_n.Enter += (s, e) => { this.ActiveControl = null; };
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string ipAddress = txt_ip.Text;
            string message = rtb_ccd.Text;

          
            if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Please enter both IP address and message.");
            }

            try
            {
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    clientSocket.Connect(IPAddress.Parse(ipAddress), DefaultPort);

                   
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    clientSocket.Send(messageBytes);

                    
                    byte[] buffer = new byte[1024];
                    int length = clientSocket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, length);

                    
                    rtb_n.AppendText("Response from CCD: " + response + Environment.NewLine);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
