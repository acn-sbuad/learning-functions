using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Models
{
    public class Pizza
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "imageSource")]
        public Uri ImageSource { get; set; }

        [JsonProperty(PropertyName = "ratingSummary")]
        public Dictionary<int, int> RatingSummary { get; set; }
    }
}
