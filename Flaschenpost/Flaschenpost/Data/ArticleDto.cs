using System.Text.Json.Serialization;

namespace Flaschenpost
{
    public class ArticleDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("pricePerUnitText")]
        public string PricePerUnitText { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
