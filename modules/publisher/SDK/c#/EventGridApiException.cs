// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.Azure.EventGridEdge.SDK
{
    public sealed class EventGridApiException : Exception
    {
        public EventGridApiException(HttpRequestMessage request, HttpResponseMessage response, string responsePayload)
            : base(GetMessage(request, response, responsePayload))
        {
            this.RequestUri = request.RequestUri;
            this.RequestMethod = request.Method.Method;
            this.ResponseStatusCode = response?.StatusCode;
            this.ResponseReasonPhrase = response?.ReasonPhrase;
            this.ResponsePayload = responsePayload;
        }

        public Uri RequestUri { get; }

        public string RequestMethod { get; }

        public HttpStatusCode? ResponseStatusCode { get; }

        public string ResponseReasonPhrase { get; }

        public string ResponsePayload { get; }

        private static string GetMessage(HttpRequestMessage request, HttpResponseMessage response, string responsePayload)
        {
            if (response != null)
            {
                return $"REQUEST: Url={request.RequestUri} Method={request.Method.Method} \nRESPONSE: statusCode={response.StatusCode} reasonPhrase=<{response.ReasonPhrase}> payload={responsePayload}";
            }
            else
            {
                return $"REQUEST: Url={request.RequestUri} Method={request.Method.Method} \nRESPONSE: <null>";
            }
        }
    }
}
