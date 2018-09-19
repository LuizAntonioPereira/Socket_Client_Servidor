using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System;

public class SocketListener{

	private bool isReady = false;
	private Thread tListener = null;
	
	private string listenerInput = string.Empty;
	
	public string StartListener(){
		isReady = true;
		ThreadStart ts = new ThreadStart(listening);
		tListener = new Thread(ts);
		tListener.Start();
		
		return listenerInput;
		
	}
	
	private void listening (){
		
		TcpListener sListener = null;
		IPAddress host = Dns.GetHostEntry("localhost").AddressList[0];
		Int32 port = 9940;
		
		try{
			
			sListener = new TcpListener(host, port);
			sListener.Start();
			
			while(isReady){
				
				if(sListener.Pending()){
					TcpClient sClient = sListener.AcceptTcpClient();
					byte[] data = new byte[256];
					NetworkStream ns = sClient.GetStream();
					ns.Read(data, 0, data.Length);
					
					listenerInput = System.Text.Encoding.ASCII.GetString(data);
					
				}else{
					Thread.Sleep(500);
				}
			}
			
		}catch(Exception e){
			listenerInput = e.InnerException.Message;
		}
		
	}

}