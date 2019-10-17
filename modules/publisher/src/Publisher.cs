// Copyright(c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventGridEdge.SDK;
using Newtonsoft.Json;

namespace Microsoft.Azure.EventGridEdge.QuickStart.Publisher
{
    public static class Publisher
    {
        // EventGrid module's URL. Needs to match the name of the Event Grid module. 
        private static readonly string eventGridBaseAddress = "http://eventgridmodule";
        
        // EventGrid HTTP Port
        private static readonly int eventGridHTTPPort = 5888;

        private static readonly string eventGridURL = $"{eventGridBaseAddress}:{eventGridHTTPPort}";

        // Time to wait before publishing events
        private static readonly TimeSpan initialDelay = TimeSpan.FromMinutes(1);

        // Name of the topic we will publish to. It needs to match with the name specified in device twin configuration of eventgrid module
        private static readonly string topicName = "quickstarttopic";
        
        public static async Task Main()
        {
            Console.WriteLine($"[INFO] - Wait a few minutes for Event Grid module to come up...");
            Thread.Sleep(initialDelay);

            Console.WriteLine($"[INFO] - EventGrid URL: {eventGridURL}");
            EventGridEdgeClient egClient = new EventGridEdgeClient(eventGridBaseAddress, eventGridHTTPPort);

            Console.WriteLine($"[INFO] - Check if topic '{topicName}' exists...");
            var createdTopic = await egClient.Topics.GetTopicAsync(topicName: topicName, CancellationToken.None).ConfigureAwait(false);
            if (createdTopic == null)
            {
               Console.WriteLine($"[ERROR] - Failed to retrieve topic '{topicName}'. Exiting.");    
               return;
            }

            Console.WriteLine($"[ERROR] - Successfully retrieved topic '{topicName}'.");    

            Console.WriteLine($"[INFO] - Start publishing events to topic '{topicName}'");
            while (true)
            {
                EventGridEvent evt = GetEvent();
                egClient.Events.PublishAsync(topicName: topicName, new List<EventGridEvent>() { evt }, CancellationToken.None).GetAwaiter().GetResult();
                Console.WriteLine($"\n[INFO] - Published event: {JsonConvert.SerializeObject(evt)}");
            }
        }

        private static EventGridEvent GetEvent()
        {
            Random random = new Random();
            string subject = $"sensor:{random.Next(1, 100)}";
            double temperature = random.NextDouble();
            double pressure = random.NextDouble();
            double humidity = random.Next(1, 25);
            return new EventGridEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Topic = topicName,
                Subject = subject,
                EventType = "sensor.temperature",
                DataVersion = "1.0",
                EventTime = DateTime.UtcNow,
                Data = new
                {
                    Machine = new { Temperature = temperature, Pressure = pressure },
                    Ambient = new { Temperature = temperature, Humidity = humidity },
                },
            };
        }
    }
}
