namespace Parallel.Deployment
{
	public class ImportRegistryKeys
	{
		private readonly string _buildToolsDirectory;
		private readonly Command _command;

		public ImportRegistryKeys(string buildToolsDirectory)
		{
			_buildToolsDirectory = buildToolsDirectory;
			_command = new Command();
		}

		public ProcessResult Do() {
			return _command.Do(string.Format(@"reg import {0}DeploymentKeyConfig\KeyRegistration.reg", _buildToolsDirectory));
		}
	}
}