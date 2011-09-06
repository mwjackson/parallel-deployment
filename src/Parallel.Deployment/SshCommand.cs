namespace Parallel.Deployment
{
	public class SshCommand
	{
		private readonly string _buildToolsDirectory;
		private readonly Command _command;

		public SshCommand(string buildToolsDirectory)
		{
			_buildToolsDirectory = buildToolsDirectory;
			_command = new Command();
		}

		public ProcessResult Do(ServerCredentials server, string command) {
			const string plink = @"plink.exe";
			const string keyfile = @"DeploymentKeyConfig\Key.ppk";
			var scpCmd = string.Format(@"{6}{0} {1}@{2} -i {6}{4} -P {3} -batch -v {5}",
			                           plink,
			                           server.User,
			                           server.Host,
			                           server.Port,
			                           keyfile,
			                           command,
			                           _buildToolsDirectory);
			return _command.Do(scpCmd);
		}
	}
}