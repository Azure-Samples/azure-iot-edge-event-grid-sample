// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.EventGridEdge.SDK
{
    public class EventsAPI
    {
        private const string ApiVersionSuffix = "?api-version=2019-01-01-preview";
        private readonly EventGridEdgeClient client;

        internal EventsAPI(EventGridEdgeClient client)
        {
            this.client = client;
        }

        public async Task PublishAsync<T>(string topicName, string eventId, T payload, CancellationToken token)
        {
            using (StreamContent streamContent = this.client.CreateJsonContent(payload))
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"topics/{UrlEncoder.Default.Encode(topicName)}/events/{eventId}{ApiVersionSuffix}") { Content = streamContent })
            using (HttpResponseMessage response = await this.client.HttpClient.SendAsync(request, token))
            {
                await response.ThrowIfFailedAsync(request);
            }
        }

        public async Task PublishAsync<T>(string topicName, IEnumerable<T> payload, CancellationToken token)
        {
            using (StreamContent streamContent = this.client.CreateJsonContent(payload))
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"topics/{UrlEncoder.Default.Encode(topicName)}/events{ApiVersionSuffix}") { Content = streamContent })
            using (HttpResponseMessage response = await this.client.HttpClient.SendAsync(request, token))
            {
                await response.ThrowIfFailedAsync(request);
            }
        }
    }
}
