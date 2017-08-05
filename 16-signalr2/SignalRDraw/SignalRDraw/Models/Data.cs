using System;
using Newtonsoft.Json;

namespace SignalRDraw.Models
{
    public class Data
    {
        // JsonProperty attribute is used for linking with the model data

        [JsonProperty("startX")]
        public float StartX { get; set; }

        [JsonProperty("startY")]
        public float StartY { get; set; }

        [JsonProperty("endX")]
        public float EndX { get; set; }

        [JsonProperty("endY")]
        public float EndY { get; set; }
    }
}
