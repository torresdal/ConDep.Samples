using System;
using System.Collections.Generic;
using System.Threading;
using Amazon;
using Amazon.ElasticLoadBalancing;
using Amazon.ElasticLoadBalancing.Model;
using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Dsl.Logging;
using ConDep.Dsl.Operations.LoadBalancer;
using ConDep.Dsl.SemanticModel;

namespace ConDepSamples.Operations.AmazonELB
{
    public class AmazonElasticLoadBalancer : ILoadBalance
    {
        private readonly LoadBalancerConfig _config;
        private readonly AmazonElasticLoadBalancingClient _client;

        public AmazonElasticLoadBalancer(LoadBalancerConfig config)
        {
            _config = config;
            var awsConfig = new AmazonElasticLoadBalancingConfig {RegionEndpoint = RegionEndpoint.EUWest1};

            _client = new AmazonElasticLoadBalancingClient(config.UserName, config.Password, awsConfig);
        }

        public void BringOffline(string serverName, string farm, LoadBalancerSuspendMethod suspendMethod, IReportStatus status)
        {
            try
            {
                var request = new DeregisterInstancesFromLoadBalancerRequest(_config.Name, new List<Instance> { new Instance(farm) });
                var respose = _client.DeregisterInstancesFromLoadBalancer(request);
            }
            catch (InvalidInstanceException instanceException)
            {
                Logger.Warn(instanceException.Message);
                Logger.Warn("Did you just add new servers to your load balancing cluster? That would be one explanation for why I can't find your instance...");
                return;
            }

            Logger.Info("Message sent to AWS to de-register instance [{0}]", farm);
            if(suspendMethod == LoadBalancerSuspendMethod.Graceful)
            {
                Logger.Info("Waiting for {0} seconds to allow existing connections to complete...");
                Thread.Sleep(60000);
            }
            else
            {
                Logger.Info("Suspend mode [{0}] is set, so not bothering to wait for any existing connections to complete.", suspendMethod);
            }
        }

        public void BringOnline(string serverName, string farm, IReportStatus status)
        {
            var request = new RegisterInstancesWithLoadBalancerRequest(_config.Name, new List<Instance> { new Instance(farm) });
            var response = _client.RegisterInstancesWithLoadBalancer(request);
            Logger.Info("Message sent to AWS to register instance [{0}]", farm);
            Logger.Info("Waiting for 60 seconds to allow instance to come online...");
            Thread.Sleep(60000);
        }

        public LbMode Mode { get; set; }
    }
}