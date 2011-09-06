using System;
using System.Text;

namespace Parallel.Deployment
{
	public class ProcessResult
	{
		public string Command { get; private set; }
		public int ExitCode { get; private set; }
		public string StdOut { get; private set; }
		public string StdErr { get; private set; }
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }

		public ProcessResult(string command, int exitCode, string stdOut, string stdErr, DateTime startTime, DateTime endTime)
		{
			ExitCode = exitCode;
			StdOut = stdOut;
			StdErr = stdErr;
			StartTime = startTime;
			EndTime = endTime;
			Command = command;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("Command:");
			sb.AppendLine(Command);
			sb.AppendLine("Exit Code:");
			sb.AppendLine(ExitCode.ToString());
			sb.AppendLine("StdOut:");
			sb.AppendLine(StdOut);
			sb.AppendLine("StdErr:");
			sb.AppendLine(StdErr);
			sb.AppendLine(string.Format("StartTime:{0} EndTime:{1}", StartTime, EndTime));
			return sb.ToString();
		}
	}
}