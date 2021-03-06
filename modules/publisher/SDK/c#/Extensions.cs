﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Azure.EventGridEdge.SDK
{
    public static class CliExtensions
    {
        public static async Task ThrowIfFailedAsync(this HttpResponseMessage response, HttpRequestMessage request)
        {
            if (!response.IsSuccessStatusCode)
            {
                string responsePayload = "!!!EMPTY PAYLOAD";
                try
                {
                    responsePayload = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    responsePayload = $"!!!payload-read-failed with inner exception:{ex}";
                }

                throw new EventGridApiException(request, response, responsePayload);
            }
        }
    }
}
