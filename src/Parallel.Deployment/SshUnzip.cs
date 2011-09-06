namespace Parallel.Deployment
{
	public class SshUnzip
	{
		private readonly string _buildToolsDirectory;
		private readonly string _buildToolsZip;
		private readonly SshCommand _sshCommand;

		public SshUnzip(string buildToolsDirectory, string buildToolsZip)
		{
			_buildToolsDirectory = buildToolsDirectory;
			_buildToolsZip = buildToolsZip;
			_sshCommand = new SshCommand(_buildToolsDirectory);
		}

		public ProcessResult Do(ServerCredentials server) {
			return _sshCommand.Do(server, string.Format(@"C:\7-Zip\7z.exe -y x -o{0} ""{1}{2}""",
			                                            _buildToolsDirectory,
			                                            server.HomeDirectory,
			                                            _buildToolsZip));
		}
	}
}