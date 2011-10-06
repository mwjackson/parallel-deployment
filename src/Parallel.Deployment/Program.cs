using System;
using System.IO;
using System.Xml.Serialization;

namespace Parallel.Deployment {
	class Program {
		private const string CONFIG_FILE = "deploymentConfig.xml";
		
		private static DeploymentConfiguration _deploymentConfiguration;

		static int Main(string[] args)
		{
			try
			{
				if (!File.Exists(CONFIG_FILE)) {
					Console.WriteLine("No deploymentConfig.xml found. Creating empty one!");
					CreateConfigFile();
					return 0;
				}

                ReadConfigFile();

			    new ParallelDeployment(_deploymentConfiguration).Do();

			    return 0;
			} 
			catch(Exception e)
			{
				Console.WriteLine(e);
				Console.ReadKey();
				return 1;
			}
		}

	    private static void ReadConfigFile()
	    {
	        var xmlSerializer = new XmlSerializer(typeof (DeploymentConfiguration));
	        _deploymentConfiguration = (DeploymentConfiguration) xmlSerializer.Deserialize(new StreamReader(CONFIG_FILE, false));
	    }

	    private static void CreateConfigFile() {
			var xmlSerializer = new XmlSerializer(typeof(DeploymentConfiguration));
			var emptyConfig = new DeploymentConfiguration();
			xmlSerializer.Serialize(new StreamWriter(CONFIG_FILE, false), emptyConfig);
		}
	}
}

