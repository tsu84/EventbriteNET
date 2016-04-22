using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventbriteNET
{
    /// <summary>
    /// Base Eventbrite Object <see cref="https://developer.eventbrite.com/docs/data-types/#object" />
    /// </summary>
    public abstract class EventbriteObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("resource_uri")]
        public string ResourceUri { get; set; }

    }

    public class Pagination
    {
        [JsonProperty("object_count")]
        public int ObjectCount { get; set; }
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        [JsonProperty("page_count")]
        public int PageCount { get; set; }

    }

   
}
