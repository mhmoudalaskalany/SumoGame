using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Sumo.Ui.Helper
{
    public class HandleClient
    {
        TcpClient _clientSocket;
        string _message;


        protected virtual void PublishEvent()
        {
            MessageBox.Show(_message);
            _clientSocket.Close();
        }

        public void StartClient(TcpClient inClientSocket, string clineNo)
        {
            _clientSocket = inClientSocket;
            _message = clineNo;
            Thread ctThread = new Thread(PublishEvent);
            ctThread.Start();
        }



    }
}