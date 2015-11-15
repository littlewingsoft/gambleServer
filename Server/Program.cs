using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;

namespace Server
{
	class MainClass
	{
		//Dictionary<int, TcpClient> clients;

		public static void Main (string[] args){
			int recv = 0;
			byte[] data = new byte[1024];

			IPEndPoint ep = new IPEndPoint(IPAddress.Any, 5882);
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			server.Bind(ep);

			Console.WriteLine("Waiting for a client... " + ep.ToString());

			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			EndPoint remoteEP = (EndPoint)sender;

			recv = server.ReceiveFrom(data, ref remoteEP);

			Console.WriteLine("[first] Message received from {0}", remoteEP.ToString());
			Console.WriteLine("[first] received data : {0}", Encoding.UTF8.GetString(data, 0, recv));

			string welcome = "Welcome to udp server";
			data = Encoding.UTF8.GetBytes(welcome);
			server.SendTo(data, remoteEP);

			while(true)
			{
				data = new byte[1024];
				recv = server.ReceiveFrom(data, ref remoteEP);
				string recvData = Encoding.UTF8.GetString(data, 0, recv);
				Console.WriteLine("received data : {0}", recvData);

				server.SendTo(Encoding.UTF8.GetBytes(recvData), remoteEP);
				Console.WriteLine("send data : {0}", Encoding.UTF8.GetString(data, 0, recv));
				Console.WriteLine("");
			}

			server.Close();
		}
		public static void aMain (string[] args)
		{
			NetworkStream stream = null;
			TcpListener tcpListener = null;
			StreamReader reader = null;
			TcpClient tc = null;

			try
			{
				//IP주소를 나타내는 객체를 생성,TcpListener를 생성시 인자로 사용할려고
				IPAddress ipAd = IPAddress.Parse("127.0.0.1");

				//TcpListener Class를 이용하여 클라이언트의 연결을 받아 들인다.
				tcpListener = new TcpListener(ipAd, 5882);
				tcpListener.Start();
				Console.WriteLine("start demon" );
				Console.WriteLine("ip:" + tcpListener.LocalEndpoint.ToString() );
				Console.WriteLine("port:" + "5882" );
				//Client의 접속이 올때 까지 Block 되는 부분, 대개 이부분을 Thread로 만들어 보내 버린다.
				//백그라운드 Thread에 처리를 맡긴다.
				tc = tcpListener.AcceptTcpClient();

				stream = tc.GetStream();
				//Encoding encode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
				reader = new StreamReader(stream, Encoding.UTF8);

				bool isQuit=false;
				do
				{
					string ret = reader.ReadLine();
					if( ret == null )
						break;

					switch(ret)
					{
					case "/totalconnectioncount":
						
						break;
					case "/echo":
						{
							byte[] buffer = Encoding.UTF8.GetBytes(ret.ToCharArray());
							stream.Write(buffer,0,buffer.Length);
							Console.WriteLine("to client :" + ret );
						}break;
					case "/quit":
						{
							isQuit =true;
							Console.WriteLine("byebye");
						}break;
					}


				}while (isQuit == false );

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			finally
			{
				tcpListener.Stop ();
				tc.Close();
			}
		}
//		static async Task MainAsync()
//		{
//	        Console.WriteLine("Starting...");
//	        var server = new TcpListener(IPAddress.Parse("0.0.0.0"), 66);
//	        server.Start();
//	        Console.WriteLine("Started.");
//	        while (true)
//	       {
//	           var client = await server.AcceptTcpClientAsync().ConfigureAwait(false);
//	           var cw = new ClientWorking(client, true);
//				cw.DoSomethingWithClientAsync ();
//	       }
//	        // :)
//	        server.Stop();
//	    }
//
//		static async void TestMethod()
//		{
//			    Console.WriteLine("Code Block #1");
//			    await Test1Async();
//			    Console.WriteLine("Code Block #2");
//			     await Test2Async();
//			     Console.WriteLine("Code Block #3");
//		}
//		 
//		static Task Test1Async()
//		{
//		     Action action = delegate()
//			     {
//			         Console.WriteLine("Task #1");
//			         System.Threading.Thread.Sleep(1000);
//			         Console.WriteLine("End of Task #1");
//			     };
//		     Task task = Task.Factory.StartNew(action);
//		     return task;
//	 	}
//		 
//		static Task Test2Async()
//		{
//		     Action action = delegate()
//			     {
//			         Console.WriteLine("Task #2");
//			         System.Threading.Thread.Sleep(1000);
//			         Console.WriteLine("End of Task #2");
//			     };
//		     Task task = Task.Factory.StartNew(action);
//		     return task;
//		}

	}
}
