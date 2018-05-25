// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SampleEphReceiver
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.EventHubs.Processor;

    public class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://tzf-int-dev-multi-001-eventhub-cgq.servicebus.windows.net/;SharedAccessKeyName=QRadarEPPolicy;SharedAccessKey=5A/MdEcO9L6hENJyGUktHhnBq1AZo2b6MPT9nEBN0Mg=;EntityPath=aztzfdma01wulogs";
        private const string EventHubName = "aztzfdma01wulogs";
        private const string StorageContainerName = "test";
        private const string StorageAccountName = "cgqtesteventhub";
        private const string StorageAccountKey = "i5Q5fuq3JEh9jd+Ivfed9BH//nfiTtl8WvlC7uG9kS95qKU3Eh5Ykgdx8CNPFdDaJTjdWDjFWe2VFssBdC08rg==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
