using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Parallel.Deployment {
	class Program {
		private const string CONFIG_FILE = "deploymentConfig.xml";
		
		private static DeploymentConfiguration _deploymentConfiguration;

		static int Main(string[] args)
		{
			try
			{
				var xmlSerializer = new XmlSerializer(typeof(DeploymentConfiguration));
				_deploymentConfiguration = (DeploymentConfiguration) xmlSerializer.Deserialize(new StreamReader(CONFIG_FILE, false));

				HandleResult(new ImportRegistryKeys(_deploymentConfiguration.BuildToolsPath).Do());

				var tasks = new List<Task>();
				_deploymentConfiguration.Servers.ForEach(server =>
					{
						var task = Task.Factory.StartNew(() => Deploy(server));
						tasks.Add(task);
					}
				);

				Task.WaitAll(tasks.ToArray());
				Console.WriteLine("Finished!");
				Console.ReadKey();
				return 0;
			} 
			catch(Exception e)
			{
				Console.WriteLine(e);
				Console.ReadKey();
				return 1;
			}
		}

		private static void Deploy(ServerCredentials server)
		{
			HandleResult(new SftpBuildToolsZip(_deploymentConfiguration.BuildToolsZipName).Do(server));
			HandleResult(new SshUnzip(_deploymentConfiguration.BuildToolsPath, _deploymentConfiguration.BuildToolsZipName).Do(server));
		}

		private static readonly object _lockObj = new object();
		private static void HandleResult(ProcessResult result) {
			lock(_lockObj)
			{
				Console.WriteLine(result);
			}
			if (result.ExitCode != 0) throw new InvalidOperationException(string.Format("Non-zero exit code from task: {0}", result.ExitCode));
		}
	}
}

