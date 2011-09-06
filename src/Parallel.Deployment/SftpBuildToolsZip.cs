namespace Parallel.Deployment
{
	public class SftpBuildToolsZip
	{
		private readonly string _buildToolsZip;
		private readonly Command _command;

		public SftpBuildToolsZip(string buildToolsZip)
		{
			_buildToolsZip = buildToolsZip;
			_command = new Command();
		}

		public ProcessResult Do(ServerCredentials server) {
			const string buildToolsDirectory = @"C:\TeamCityBuildTools\";
			const string winscp = @"winscp\winscp.com";
			const string keyfile = @"Key.ppk";
			var scpCmd = string.Format(@"{6}{0} /command ""open sftp://{1}@{2}:{3} -hostkey=""""{7}"""" -privatekey={6}{4}"" ""rm {5}"" ""put {6}{5}"" ""exit""",
			                           winscp,
			                           server.User,
			                           server.Host,
			                           server.Port,
			                           keyfile,
			                           _buildToolsZip,
			                           buildToolsDirectory, server.HostKey);
			return _command.Do(scpCmd);
		}
	}
}