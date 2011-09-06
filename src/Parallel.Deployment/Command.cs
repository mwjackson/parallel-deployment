using System;
using System.Diagnostics;
using System.IO;

namespace Parallel.Deployment
{
	public class Command
	{
		public ProcessResult Do(string command) {
			var process = new Process {
				StartInfo =
					new ProcessStartInfo("cmd", string.Format("/C {0}", command)) {
						RedirectStandardError = true,
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						ErrorDialog = false,
						UseShellExecute = false
						}
			};

			var startTime = DateTime.Now;
			process.Start();
			StreamReader outputReader = process.StandardOutput;
			StreamReader errorReader = process.StandardError;
			process.WaitForExit();
			var endTime = DateTime.Now;
			return new ProcessResult(command, process.ExitCode, outputReader.ReadToEnd(), errorReader.ReadToEnd(), startTime, endTime);
		}
	}
}