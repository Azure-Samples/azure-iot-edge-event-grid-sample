// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;

namespace Microsoft.Azure.EventGridEdge.SDK
{
    public class EventGridEvent : EventGridEvent<object>
    {
    }

    public class EventGridEvent<T> : IEquatable<EventGridEvent>
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public string Subject { get; set; }

        public string EventType { get; set; }

        public string DataVersion { get; set; }

        public string MetadataVersion { get; set; }

        public DateTime EventTime { get; set; }

        public T Data { get; set; }

        public bool Equals(EventGridEvent other)
        {
            return StringEquals(this.Id, other.Id) &&
                StringEquals(this.Topic, other.Topic) &&
                StringEquals(this.Subject, other.Subject) &&
                StringEquals(this.EventType, other.EventType) &&
                StringEquals(this.DataVersion, other.DataVersion) &&
                StringEquals(this.MetadataVersion, other.MetadataVersion) &&
                this.EventTime.Equals(other.EventTime);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            else if (obj is EventGridEvent ege)
            {
                return this.Equals(ege);
            }

            return false;
        }

        public override int GetHashCode() => this.Id?.GetHashCode() ?? base.GetHashCode();

        private static bool StringEquals(string str1, string str2) => (str1 == null && str2 == null) || (str1 != null && str2 != null && str1.Equals(str2, StringComparison.Ordinal));
    }
}
