using System.Collections;
using System.Net.Sockets;

public class SocketClient{
	
	public void connect(string host, int port, string message){
		
		TcpClient sClient = new TcpClient(host,port);
		byte[] data = new byte[256];
		data = System.Text.Encoding.ASCII.GetBytes(message);
		NetworkStream ns = sClient.GetStream();
		ns.Write(data, 0, data.Length);
		
	}
}