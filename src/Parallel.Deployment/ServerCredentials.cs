using System;
using System.Text;

namespace Parallel.Deployment
{

	[Serializable]
	public class ServerCredentials
	{
		public string Host { get; set; }
		public string User { get; set; }
		public int Port { get; set; }
		public string HostKey { get; set; }
		public string HomeDirectory { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("{0}@{1}:{2}", User, Host, Port);
			sb.AppendFormat("{0}", HostKey);
			sb.AppendFormat("{0}", HomeDirectory);
			return sb.ToString();
		}
	}
}