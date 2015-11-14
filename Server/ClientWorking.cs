using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
	class ClientWorking
	{
	    TcpClient _client;
	    bool _ownsClient;
	
	    public ClientWorking(TcpClient client, bool ownsClient)
	    {
		        _client = client;
		        _ownsClient = ownsClient;
		    }
	
	    public async Task DoSomethingWithClientAsync()
	    {
		        try
		        {
			            using (var stream = _client.GetStream())
				            {
				                using (var sr = new StreamReader(stream))
				                using (var sw = new StreamWriter(stream))
					                {
					                    await sw.WriteLineAsync("Hi. This is x2 TCP/IP easy-to-use server").ConfigureAwait(false);
					                    await sw.FlushAsync().ConfigureAwait(false);
					                    var data = default(string);
					                    while (!((data = await sr.ReadLineAsync().ConfigureAwait(false)).Equals("exit", StringComparison.OrdinalIgnoreCase)))
		                    {
		                        await sw.WriteLineAsync(data).ConfigureAwait(false);
		                        await sw.FlushAsync().ConfigureAwait(false);
		                    }
		                }
		
		            }
		        }
		        finally
		        {
		            if (_ownsClient && _client != null)
		            {
		                (_client as IDisposable).Dispose();
		                _client = null;
		            }
		        }
		}
	}

	
}

