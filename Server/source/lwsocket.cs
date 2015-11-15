using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
	//시러 이렇게 하 말자 
	public class netMessage{
		public byte[] buffer;
		public string msg;


	}

	public class lwsocket
	{
		public System.Net.Sockets.TcpClient tcpSock;
		public System.Net.Sockets.UdpClient udpSock;

		public lwsocket(){
			
		}

		void doConnect(string ip, int port){
		}

		void doDisconnect(){
		}

		void sendMsg(netMessage msg){
		}

		delegate void delegate_onMessage(netMessage msg);
		delegate void delegate_onConnected();
		delegate void delegate_onDisconnected(int arg);

	}
}

