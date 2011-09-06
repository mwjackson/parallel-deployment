using System;
using System.Collections.Generic;

namespace Parallel.Deployment
{
	[Serializable]
	public class DeploymentConfiguration
	{
		public string BuildToolsPath { get; set; }
		public string BuildToolsZipName { get; set; }

		public List<ServerCredentials> Servers { get; set; }
	}
}