﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Base.Remote;
using Automatica.Core.Base.Templates;
using Automatica.Core.Driver;
using Automatica.Core.Driver.Loader;
using Automatica.Core.Plugin.Standalone.Abstraction;
using Automatica.Core.Plugin.Standalone.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace Automatica.Core.Plugin.Standalone
{
    internal class IdObject
    {
        public Guid Id { get; set; }
    }

    internal class MqttConnection : IDriverConnection
    {
        private readonly IServiceProvider _serviceProvider;

        public INodeTemplateFactory NodeTemplateFactory { get; }

        public string MasterAddress { get; }
        public string NodeId { get; }
        public string Username { get; }
        public string Password { get; }
        public ILogger Logger { get; }

        internal MqttDispatcher Dispatcher { get; }

        public IMqttClient MqttClient => _mqttClient;
        private readonly IMqttClient _mqttClient;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(0);

        public IDriverFactoryLoader Loader { get; }

        public IDriverNodesStore NodeStore { get; }

        internal IDriver DriverInstance
        {
            get => _driverInstance;
            set => _driverInstance = value;
        }
        private IDriver _driverInstance;

        public IDriverStore DriverStore { get; }

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public MqttConnection(ILogger logger, string host, string nodeId, string username, string password,
            IDriverFactory factory,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            Logger = logger;
            MasterAddress = host;
            NodeId = nodeId;
            Username = username;
            Password = password;
            Factory = factory;

            NodeTemplateFactory = serviceProvider.GetRequiredService<INodeTemplateFactory>();
            Loader = _serviceProvider.GetRequiredService<IDriverFactoryLoader>();
            NodeStore = serviceProvider.GetRequiredService<IDriverNodesStore>();
            DriverStore = serviceProvider.GetRequiredService<IDriverStore>();

            _mqttClient = new MqttFactory().CreateMqttClient();
            Dispatcher = new MqttDispatcher(_mqttClient);
        }

        public IDriverFactory Factory { get; }

        public async Task<bool> Start()
        {
            try
            {
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(NodeId)
                    .WithTcpServer(MasterAddress)
                    .WithCredentials(Username, Password)
                    .WithCleanSession()
                    .Build();
                _mqttClient.DisconnectedHandler = new MqttDisconnectedHandler(this);

                await _mqttClient.ConnectAsync(options);

                Logger.LogInformation($"Connected to mqtt broker {MasterAddress} with clientId {NodeId}");

                Logger.LogDebug($"Subscribe to {RemoteTopicConstants.CONFIG_TOPIC}/{NodeId}");
                Logger.LogDebug($"Subscribe to {RemoteTopicConstants.DISPATCHER_TOPIC}/#");

                await _mqttClient.SubscribeAsync(
                    new TopicFilterBuilder().WithTopic($"{RemoteTopicConstants.CONFIG_TOPIC}/{NodeId}")
                        .WithExactlyOnceQoS().Build(),
                    new TopicFilterBuilder().WithTopic($"{RemoteTopicConstants.DISPATCHER_TOPIC}/#")
                        .WithAtLeastOnceQoS().Build());
            }
            catch (Exception e)
            {

                Logger.LogError(e, "Could not connect to broker");
                return false;
            }

            return true;
        }

        public Task Run()
        {
            return _semaphoreSlim.WaitAsync();
        }

        public async Task<bool> Send(string topic, object data)
        {
            if (_driverInstance == null)
            {
                return false;
            }

            var jsonMessage = JsonConvert.SerializeObject(data);

            var mqttMessage = new MqttApplicationMessage
            {
                Topic = $"{RemoteTopicConstants.ACTION_TOPIC_START}/{_driverInstance.Id}/{topic}",
                Payload = Encoding.UTF8.GetBytes(jsonMessage),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce

            };

            await _mqttClient.PublishAsync(mqttMessage);

            return true;
        }

        internal void OnClientDisconnected(MqttClientDisconnectedEventArgs e)
        {
          
            Logger.LogWarning(e.Exception, "Mqtt client disconnected");
            _semaphoreSlim.Release(1);
        }

        public Task<bool> Stop()
        {
            _semaphoreSlim.Release(1);
            return Task.FromResult(true);
        }
    }
}

