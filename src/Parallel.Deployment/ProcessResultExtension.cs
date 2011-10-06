using System;

namespace Parallel.Deployment
{
    public static class ProcessResultExtension
    {
        private static readonly object _lockObj = new object();
        public static void HandleResult(this ProcessResult result)
        {
            lock (_lockObj)
            {
                Console.WriteLine(result);
            }
            if (result.ExitCode != 0) throw new InvalidOperationException(string.Format("Non-zero exit code from task: {0}", result.ExitCode));
        }
    }
}