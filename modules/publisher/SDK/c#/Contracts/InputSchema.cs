﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Microsoft.Azure.EventGridEdge.SDK
{
    public enum InputSchema
    {
        /// <summary>
        /// Events will be published in the Event Grid event schema.
        /// </summary>
        EventGridSchema = 0,

        /// <summary>
        /// Event Payloads are treated as byte arrays without any assumptions about their structure/format,
        /// and thus not validated / parsed / checked for errors.
        /// </summary>
        CustomEventSchema,
    }
}
