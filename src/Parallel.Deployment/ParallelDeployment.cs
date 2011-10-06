using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel.Deployment
{
    public class ParallelDeployment
    {
        private readonly DeploymentConfiguration _deploymentConfiguration;

        public ParallelDeployment(DeploymentConfiguration deploymentConfiguration)
        {
            _deploymentConfiguration = deploymentConfiguration;
        }

        public void Do()
        {
            new ImportRegistryKeys(_deploymentConfiguration.BuildToolsPath).Do().HandleResult();

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
        }

        private void Deploy(ServerCredentials server)
        {
            new SftpBuildToolsZip(_deploymentConfiguration.BuildToolsPath, _deploymentConfiguration.BuildToolsZipName).Do(server).HandleResult();
            new SshUnzip(_deploymentConfiguration.BuildToolsPath, _deploymentConfiguration.BuildToolsZipName).Do(server).HandleResult();
        }
    }
}